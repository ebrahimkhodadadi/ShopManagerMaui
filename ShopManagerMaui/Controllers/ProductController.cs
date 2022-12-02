
using System.Threading;

namespace Api.Controllers;

public class ProductController : BaseController
{
    protected readonly IRepository<Products> Repository;

    public ProductController(IRepository<Products> repository)
    {
        Repository = repository;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var list = await Repository.TableNoTracking.ToListAsync(cancellationToken);

        return Ok(list);
    }

    [HttpGet("GetByID")]
    public async Task<ActionResult> GetByID(int id, CancellationToken cancellationToken)
    {
        var result = await Repository.TableNoTracking
                        .Include(x => x.ProductDetails)
                        .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
