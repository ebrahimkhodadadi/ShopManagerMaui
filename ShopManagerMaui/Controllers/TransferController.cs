using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Api.Controllers
{
    public class TransferController : BaseController
    {
        protected readonly IRepository<TransportLogger> RepositoryTransfer;
        protected readonly IRepository<TransferLoggerDetail> RepositoryTransferLoggerDetail;
        protected readonly IRepository<Basket> RepositoryBasket;

        public TransferController(IRepository<TransportLogger> repositoryTransfer, IRepository<Basket> repositoryBasket, IRepository<TransferLoggerDetail> repositoryTransferLoggerDetail)
        {
            RepositoryTransfer = repositoryTransfer;
            RepositoryBasket = repositoryBasket;
            RepositoryTransferLoggerDetail = repositoryTransferLoggerDetail;
        }

        [HttpPost("TransferBasket")]
        public async Task<ActionResult> TransferBasket([FromBody] TransferBasketDto transferBasket, CancellationToken cancellationToken)
        {
            var basketList = await RepositoryBasket.TableNoTracking
                .Where(x => x.UserId == transferBasket.UserId)
                .Include(x => x.StoredProduct)
                .Include(x => x.StoredProduct.Product)
                .Include(x => x.StoredProduct.Store)
                .Select(x => new { Store = x.StoredProduct.Store, Products = x.StoredProduct.Product, Count = x.Count })
                .ToListAsync(cancellationToken);

            await RepositoryTransfer.AddAsync(new TransportLogger
            {
                Title = $"انتقال از {basketList.First().Store.Name} به {transferBasket.DestinaionStore.Name}",
                Description = transferBasket.Description,
                ProductList = basketList.Select(x => x.Products.Id).ToArray().ToString(),
                Count = basketList.Count(),
                SourceStoreId = basketList.First().Store.Id,
                DestinationStoreId = transferBasket.DestinaionStore.Id,
                TransPorterId = transferBasket.UserId
            }, cancellationToken);

            var transferId = await RepositoryTransfer.TableNoTracking.OrderByDescending(x => x.Id).Select(x => x.Id).FirstAsync();
            await RepositoryTransferLoggerDetail.AddRangeAsync(basketList.Select(x => new TransferLoggerDetail
            {
                Count = x.Count,
                TransferLoggerId = transferId,
                ProductId = x.Products.Id
            }), cancellationToken);

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
                .Where(x => x.TransferLogger.TransPorterId == userID && x.Id == transferID)
                .Include(x => x.Product)
                .Select(x => new KeyValues { Key = x.Product.Name, Value = x.Count.ToString() })
                .ToListAsync(cancellationToken);
        }
    }
}
