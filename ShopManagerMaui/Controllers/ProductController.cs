
namespace Api.Controllers;

public class ProductController : BaseController
{
    protected readonly IRepository<StoredProducts> RepositoryStoredProduct;
    protected readonly IRepository<Products> RepositoryProduct;
    protected readonly IRepository<ProductDetails> RepositoryProductDetails;

    public ProductController(IRepository<Products> repository, IRepository<ProductDetails> repositoryProductDetails, IRepository<StoredProducts> storedProduct)
    {
        RepositoryProduct = repository;
        RepositoryProductDetails = repositoryProductDetails;
        RepositoryStoredProduct = storedProduct;
    }

    [HttpGet("GetAll/{id}")]
    public async Task<ActionResult> GetAll(int id, CancellationToken cancellationToken)
    {
        var listStoredProduct = RepositoryStoredProduct.TableNoTracking.Include(x => x.Product)
            .Where(x => x.StoreId == id).Select(x => x.Product).ToList();

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
}
