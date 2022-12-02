
using Api.Base;
using System.Threading;

namespace Api.Controllers;

public class ProductController : BaseController
{
    protected readonly IRepository<Products> RepositoryProduct;
    protected readonly IRepository<ProductDetails> RepositoryProductDetails;

    public ProductController(IRepository<Products> repository, IRepository<ProductDetails> repositoryProductDetails)
    {
        RepositoryProduct = repository;
        RepositoryProductDetails = repositoryProductDetails;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var list = await RepositoryProduct.TableNoTracking.ToListAsync(cancellationToken);

        return Ok(list);
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
