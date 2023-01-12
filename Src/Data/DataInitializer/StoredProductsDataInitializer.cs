
namespace Data.DataInitializer;

public class StoredProductsDataInitializer : IDataInitializer
{
    private readonly IRepository<StoredProducts> repository;

    public StoredProductsDataInitializer(IRepository<StoredProducts> repository)
    {
        this.repository = repository;
    }

    public void InitializeData()
    {
        if (!repository.TableNoTracking.Any(p => (p.ProductId == 1 || p.ProductId == 2) && p.StoreId == 1))
        {
            repository.Add(new StoredProducts()
            {
                ProductId = 1,
                StoreId = 1,
                Count = 10,
            });
            repository.Add(new StoredProducts()
            {
                ProductId = 2,
                StoreId = 1,
                Count = 12,
            });
        }
        if (!repository.TableNoTracking.Any(p => (p.ProductId == 1 || p.ProductId == 3) && p.StoreId == 2))
        {
            repository.Add(new StoredProducts()
            {
                ProductId = 1,
                StoreId = 2,
                Count = 5,
            });
            repository.Add(new StoredProducts()
            {
                ProductId = 3,
                StoreId = 2,
                Count = 7,
            });
        }
        if (!repository.TableNoTracking.Any(p => (p.ProductId == 2 || p.ProductId == 3) && p.StoreId == 3))
        {
            repository.Add(new StoredProducts()
            {
                ProductId = 2,
                StoreId = 3,
                Count = 3,
            });
            repository.Add(new StoredProducts()
            {
                ProductId = 3,
                StoreId = 3,
                Count = 4,
            });
        }
    }
}