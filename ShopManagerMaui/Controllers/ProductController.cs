
using Entities;

namespace Api.Controllers;

public class ProductController : BaseController
{
    protected readonly IRepository<Basket> RepositoryBasket;
    protected readonly IRepository<StoredProducts> RepositoryStoredProduct;
    protected readonly IRepository<Products> RepositoryProduct;
    protected readonly IRepository<ProductDetails> RepositoryProductDetails;
    protected readonly IRepository<StoredProducts> RepositoryStoredProducts;

    public ProductController(IRepository<Products> repository, IRepository<ProductDetails> repositoryProductDetails, IRepository<StoredProducts> storedProduct, IRepository<Basket> basket, IRepository<StoredProducts> storedProducts)
    {
        RepositoryProduct = repository;
        RepositoryProductDetails = repositoryProductDetails;
        RepositoryStoredProduct = storedProduct;
        RepositoryBasket = basket;
        RepositoryStoredProducts = storedProducts;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll(int storeID, int userID, CancellationToken cancellationToken)
    {
        var basketProduct = await RepositoryBasket.TableNoTracking.Where(x => x.UserId == userID)
            .Select(x => x.StoredProduct.ProductId)
            .ToListAsync(cancellationToken);
        
        var listStoredProduct = await RepositoryStoredProduct.TableNoTracking
            .Include(x => x.Product)
            .Where(x => x.StoreId == storeID && !basketProduct.Contains(x.ProductId))
            .Select(x => x.Product)
            .ToListAsync(cancellationToken);

        return Ok(listStoredProduct);
    }

    [HttpGet("GetByID")]
    public async Task<ActionResult> GetByID(int id, CancellationToken cancellationToken)
    {
        var products = await RepositoryProduct.TableNoTracking
                        .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
        
        if (products == null)
            return NotFound();

        var productDetail = await RepositoryProductDetails.TableNoTracking
            .Where(p => p.ProductId == products.Id).ToListAsync();

        return Ok(new { Product = products, ProductDetail = productDetail });
    }

    [HttpGet("AddToBasket")]
    public async Task<ActionResult> AddToBasket(int storeID, int productId, int userID, CancellationToken cancellationToken)
    {
        var products = await RepositoryProduct.TableNoTracking
                        .SingleOrDefaultAsync(p => p.Id.Equals(productId), cancellationToken);

        if (products == null)
            return NotFound();

        var storedProduct = await RepositoryStoredProducts.TableNoTracking.SingleAsync(x => x.StoreId == storeID && x.ProductId == productId, cancellationToken);

        var checkBasket = await RepositoryBasket.TableNoTracking.Where(x => x.UserId == userID && x.StoredProduct.StoreId != storeID).AnyAsync(cancellationToken);
        if (checkBasket)
            return Ok(false);
        
        await RepositoryBasket.AddAsync(new Basket()
        {
            StoredProductId = storedProduct.Id,
            UserId = userID,
            Count = 1
        }, cancellationToken);

        return Ok(true);
    }
}
