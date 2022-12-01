namespace Entities;

public class StoredProducts : BaseEntity
{
    public int Count { get; set; }
    
    #region Foreign Key
    public Products Product { get; set; }
    public int ProductId { get; set; }

    public Stores Store { get; set; }
    public int StoreId { get; set; }
    #endregion
}
