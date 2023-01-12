namespace Entities;
public class Products : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    public string? Image { get; set; }

    #region Foreign Key
    public ICollection<ProductDetails> ProductDetails { get; set; }
    public ICollection<StoredProducts> StoredProducts { get; set; }
    #endregion
}
