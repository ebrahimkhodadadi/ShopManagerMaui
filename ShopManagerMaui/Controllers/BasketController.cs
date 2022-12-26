
using Shared.Models;

namespace Api.Controllers
{
    public class BasketController : BaseController
    {
        protected readonly IRepository<Basket> RepositoryBasket;

        public BasketController(IRepository<Basket> repository)
        {
            RepositoryBasket = repository;
        }

        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult> GetAll(int id, CancellationToken cancellationToken)
        {
            var list = await RepositoryBasket.TableNoTracking
                .Include(x => x.StoredProduct)
                .Include(x => x.StoredProduct.Product)
                .Where(x => x.UserId == id)
                .Select(x => new BasketListDto
                {
                    Id = x.Id,
                    BasketCount = x.Count,
                    UserId = x.UserId,
                    ProductId = x.StoredProduct.ProductId,
                    Name = x.StoredProduct.Product.Name,
                    Image = x.StoredProduct.Product.Image,
                    StoreProductID = x.StoredProductId,
                    StoredProductCount = x.StoredProduct.Count,
                    StoreID = x.StoredProduct.StoreId
                })
                .ToListAsync(cancellationToken);

            return Ok(list);
        }

        [HttpPost("UpdateBasket")]
        public async Task<ActionResult> UpdateBasket([FromBody] List<BasketDto> baskets, CancellationToken cancellationToken)
        {
            if(baskets.Any(x => x.Count == 0))
            {
                var emptyBaskets = baskets.Where(x => x.Count == 0).Select(x => new Basket() { Id = x.Id }).ToList();
                await RepositoryBasket.DeleteRangeAsync(emptyBaskets, cancellationToken);
            }
                
            await RepositoryBasket.UpdateRangeAsync(baskets.Where(x => x.Count > 0).Select(x => new Basket()
            {
                Id = x.Id,
                Count = x.Count,
                UserId = x.UserId,
                StoredProductId = x.StoreProductID
            }), cancellationToken);

            return RedirectToAction(nameof(GetAll), new { id = baskets.First().UserId });
        }
    }
}
