
namespace Data.DataInitializer;

public class ProductDataInitializer : IDataInitializer
{
    private readonly IRepository<Products> repository;

    public ProductDataInitializer(IRepository<Products> repository)
    {
        this.repository = repository;
    }

    public void InitializeData()
    {
        if (!repository.TableNoTracking.Any(p => p.Name == "موبایل هاوایی"))
        {
            repository.Add(new Products()
            {
                Name = "موبایل هاوایی",
                Image = "mobile.png"
            });
        }
        if (!repository.TableNoTracking.Any(p => p.Name == "لپ تاپ asus"))
        {
            repository.Add(new Products()
            {
                Name = "لپ تاپ asus",
            });
        }
        if (!repository.TableNoTracking.Any(p => p.Name == "فرش"))
        {
            repository.Add(new Products()
            {
                Name = "فرش",
                Image = "carpet.png"
            });
        }
    }
}