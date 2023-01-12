using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Api.Controllers
{
    public class TransferController : BaseController
    {
        protected readonly IRepository<TransportLogger> RepositoryTransfer;
        protected readonly IRepository<TransferLoggerDetail> RepositoryTransferLoggerDetail;
        protected readonly IRepository<Basket> RepositoryBasket;
        protected readonly IRepository<StoredProducts> RepositoryStoredProduct;

        public TransferController(IRepository<StoredProducts> repositoryStoredProduct, IRepository<TransportLogger> repositoryTransfer, IRepository<Basket> repositoryBasket, IRepository<TransferLoggerDetail> repositoryTransferLoggerDetail)
        {
            RepositoryTransfer = repositoryTransfer;
            RepositoryBasket = repositoryBasket;
            RepositoryTransferLoggerDetail = repositoryTransferLoggerDetail;
            RepositoryStoredProduct = repositoryStoredProduct;
        }

        [HttpPost("TransferBasket")]
        public async Task<ActionResult> TransferBasket([FromBody] TransferBasketDto transferBasket, CancellationToken cancellationToken)
        {
            // get basket list
            var basketList = await RepositoryBasket.TableNoTracking
                .Where(x => x.UserId == transferBasket.UserId)
                .Include(x => x.StoredProduct)
                .Include(x => x.StoredProduct.Product)
                .Include(x => x.StoredProduct.Store)
                .Select(x => new { Basket = x, Store = x.StoredProduct.Store, Products = x.StoredProduct.Product, Count = x.Count })
                .ToListAsync(cancellationToken);

            // change count of StoredProducts
            var listOfStoredProduct = await RepositoryStoredProduct.Entities.Where(x => x.StoreId == basketList.First().Store.Id && basketList.Select(x => x.Products.Id).Contains(x.ProductId)).ToListAsync(cancellationToken);
            listOfStoredProduct.ForEach(x => x.Count -= basketList.First(y => y.Products.Id == x.ProductId).Count);
            await RepositoryStoredProduct.UpdateRangeAsync(listOfStoredProduct, cancellationToken);
            listOfStoredProduct = await RepositoryStoredProduct.Entities.Where(x => x.Count <= 0).ToListAsync(cancellationToken);
            if (listOfStoredProduct.Any())
                await RepositoryStoredProduct.DeleteRangeAsync(listOfStoredProduct, cancellationToken);

            listOfStoredProduct = await RepositoryStoredProduct.Entities.Where(x => x.StoreId == transferBasket.DestinaionStoreID).Include(x => x.Product).ToListAsync(cancellationToken);
            foreach (var item in basketList.Select(x => x.Products))
            {
                var count = basketList.First(x => x.Products.Id == item.Id).Count;
                if (!listOfStoredProduct.Select(x => x.Product.Id).Contains(item.Id))
                    await RepositoryStoredProduct.AddAsync(new StoredProducts() { Count = count, ProductId = item.Id, StoreId = transferBasket.DestinaionStoreID }, cancellationToken);
                else
                {
                    var storedProduct = listOfStoredProduct.First(x => x.ProductId == item.Id);
                    storedProduct.Count += count;
                    await RepositoryStoredProduct.UpdateAsync(storedProduct, cancellationToken);
                }
            }


            // add to transfers
            await RepositoryTransfer.AddAsync(new TransportLogger
            {
                Title = $"انتقال از {basketList.First().Store.Name} به {transferBasket.DestinaionStoreName}",
                Description = transferBasket.Description,
                Count = basketList.Count(),
                SourceStoreId = basketList.First().Store.Id,
                DestinationStoreId = transferBasket.DestinaionStoreID,
                TransPorterId = transferBasket.UserId
            }, cancellationToken);

            // add products transfer detail
            var transferId = await RepositoryTransfer.Entities.OrderByDescending(x => x.Id).Select(x => x.Id).FirstAsync();
            await RepositoryTransferLoggerDetail.AddRangeAsync(basketList.Select(x => new TransferLoggerDetail
            {
                Count = x.Count,
                TransferLoggerId = transferId,
                ProductId = x.Products.Id
            }), cancellationToken);

            // delete all from basket
            await RepositoryBasket.DeleteRangeAsync(RepositoryBasket.Entities.Where(x => x.UserId == transferBasket.UserId), cancellationToken);

            return Ok(true);
        }

        [HttpGet("GetAllTransfers/{id}")]
        public async Task<List<TransportLogger>> GetAllTransfers(int id, CancellationToken cancellationToken)
        {
            return await RepositoryTransfer.TableNoTracking
                .Where(x => x.TransPorterId == id)
                .ToListAsync(cancellationToken);
        }

        [HttpGet("GetTransferDetails")]
        public async Task<List<KeyValues>> GetTrasnferDetails(int userID, int transferID, CancellationToken cancellationToken)
        {
            return await RepositoryTransferLoggerDetail.TableNoTracking
                .Include(x => x.TransferLogger)
                .Include(x => x.Product)
                .Where(x => x.TransferLogger.TransPorterId == userID && x.TransferLoggerId == transferID)
                .Select(x => new KeyValues { Key = x.Product.Name, Value = x.Count.ToString() })
                .ToListAsync(cancellationToken);
        }
    }
}
