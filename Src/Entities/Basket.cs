
namespace Entities;

public class Basket : BaseEntity
{
    public int Count { get; set; }

    #region Foreign Key
    public StoredProducts StoredProduct { get; set; }
    public int StoredProductId { get; set; }

    public Users User { get; set; }
    public int UserId { get; set; }
    #endregion
}
