
namespace Data.DataInitializer;

public class StoresDataInitializer : IDataInitializer
{
    private readonly IRepository<Stores> repository;

    public StoresDataInitializer(IRepository<Stores> repository)
    {
        this.repository = repository;
    }

    public void InitializeData()
    {
        if (!repository.TableNoTracking.Any(p => p.Name == "دیجیکالا"))
        {
            repository.Add(new Stores()
            {
                Name = "دیجیکالا",
            });
        }
        if (!repository.TableNoTracking.Any(p => p.Name == "emalls"))
        {
            repository.Add(new Stores()
            {
                Name = "emalls",
            });
        }
        if (!repository.TableNoTracking.Any(p => p.Name == "ترب"))
        {
            repository.Add(new Stores()
            {
                Name = "ترب",
            });
        }
    }
}