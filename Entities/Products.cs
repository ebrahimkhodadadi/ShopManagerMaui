namespace Entities;
public class Products : BaseEntity
{
    public string Name { get; set; }

    #region Foreign Key
    public ICollection<ProductDetails> ProductDetails { get; set; }
    public ICollection<StoredProducts> StoredProducts { get; set; }
    #endregion
}
