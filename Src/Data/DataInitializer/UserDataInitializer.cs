
namespace Data.DataInitializer;
public class UserDataInitializer : IDataInitializer
{
    private readonly IRepository<Users> repository;

    public UserDataInitializer(IRepository<Users> repository)
    {
        this.repository = repository;
    }

    public void InitializeData()
    {
        if (!repository.TableNoTracking.Any(p => p.Name == "Admin"))
        {
            repository.Add(new Users
            {
                Name = "Admin"
            });
        }
    }
}
