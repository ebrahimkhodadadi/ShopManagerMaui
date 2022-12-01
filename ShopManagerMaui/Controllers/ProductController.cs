
namespace Api.Controllers;

public class ProductController : BaseController
{
    protected readonly IRepository<Products> Repository;

    public ProductController(IRepository<Products> repository)
    {
        Repository = repository;
    }
    //// GET: ProductController
    [HttpGet]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var list = await Repository.TableNoTracking.ToListAsync(cancellationToken);

        return Ok(list);
    }

    //// GET: ProductController/Details/5
    //public ActionResult Details(int id)
    //{
    //    return View();
    //}

    //// GET: ProductController/Create
    //public ActionResult Create()
    //{
    //    return View();
    //}

    //// POST: ProductController/Create
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create(IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}

    //// GET: ProductController/Edit/5
    //public ActionResult Edit(int id)
    //{
    //    return View();
    //}

    //// POST: ProductController/Edit/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}

    //// GET: ProductController/Delete/5
    //public ActionResult Delete(int id)
    //{
    //    return View();
    //}

    //// POST: ProductController/Delete/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Delete(int id, IFormCollection collection)
    //{
    //    try
    //    {
    //        return RedirectToAction(nameof(Index));
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}
}
