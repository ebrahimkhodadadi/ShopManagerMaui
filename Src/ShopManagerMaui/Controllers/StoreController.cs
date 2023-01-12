using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class StoreController : BaseController
    {
        protected readonly IRepository<Stores> RepositoryStore;

        public StoreController(IRepository<Stores> repository)
        {
            RepositoryStore = repository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var list = await RepositoryStore.TableNoTracking.ToListAsync(cancellationToken);

            return Ok(list);
        }
    }
}
