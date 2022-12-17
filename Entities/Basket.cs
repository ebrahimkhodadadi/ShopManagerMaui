
namespace Entities;

public class Basket : BaseEntity
{
    public int Count { get; set; }

    #region Foreign Key
    public Products Product { get; set; }
    public int ProductId { get; set; }

    public Users User { get; set; }
    public int UserrId { get; set; }
    #endregion
}
