
namespace Entities;

public class ProductDetails : BaseEntity
{
    public byte Type { get; set; }
    public string Value { get; set; }

    #region Foreign Key
    public Products Product { get; set; }
    public int ProductId { get; set; }
    #endregion
}
