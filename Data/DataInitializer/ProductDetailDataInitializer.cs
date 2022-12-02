
namespace Data.DataInitializer;

public class ProductDetailDataInitializer : IDataInitializer
{
    private readonly IRepository<ProductDetails> repository;

    public ProductDetailDataInitializer(IRepository<ProductDetails> repository)
    {
        this.repository = repository;
    }

    public void InitializeData()
    {
        // "موبایل هاوایی"
        if (!repository.TableNoTracking.Any(p => p.ProductId == 1))
        {
            repository.Add(new ProductDetails()
            {
                ProductId = 1,
                Type = (byte)ProductDetailsEnum.Price,
                Value = "1000000"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 1,
                Type = (byte)ProductDetailsEnum.Brand,
                Value = "Huawei"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 1,
                Type = (byte)ProductDetailsEnum.Color,
                Value = "Black"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 1,
                Type = (byte)ProductDetailsEnum.Size,
                Value = "5.5"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 1,
                Type = (byte)ProductDetailsEnum.Weight,
                Value = "150"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 1,
                Type = (byte)ProductDetailsEnum.Description,
                Value = "موبایل هوآوی مدل P30"
            });
        }

        // "لپ تاپ asus"
        if (!repository.TableNoTracking.Any(p => p.ProductId == 2))
        {
            repository.Add(new ProductDetails()
            {
                ProductId = 2,
                Type = (byte)ProductDetailsEnum.Price,
                Value = "1000000"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 2,
                Type = (byte)ProductDetailsEnum.Brand,
                Value = "Asus"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 2,
                Type = (byte)ProductDetailsEnum.Color,
                Value = "Black"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 2,
                Type = (byte)ProductDetailsEnum.Size,
                Value = "15.5"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 2,
                Type = (byte)ProductDetailsEnum.Weight,
                Value = "1500"
            });
        }

        // "فرش"
        if (!repository.TableNoTracking.Any(p => p.ProductId == 3))
        {
            repository.Add(new ProductDetails()
            {
                ProductId = 3,
                Type = (byte)ProductDetailsEnum.Price,
                Value = "1000000"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 3,
                Type = (byte)ProductDetailsEnum.Brand,
                Value = "Asus"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 3,
                Type = (byte)ProductDetailsEnum.Color,
                Value = "Black"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 3,
                Type = (byte)ProductDetailsEnum.Size,
                Value = "15.5"
            });
            repository.Add(new ProductDetails()
            {
                ProductId = 3,
                Type = (byte)ProductDetailsEnum.Weight,
                Value = "1500"
            });
        }
    }
}