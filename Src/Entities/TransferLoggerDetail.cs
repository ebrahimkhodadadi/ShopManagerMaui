
namespace Entities;

public class TransferLoggerDetail : BaseEntity
{
    public int Count { get; set; }

    #region Foreign Key
    public TransportLogger TransferLogger { get; set; }
    public int TransferLoggerId { get; set; }

    public Products Product { get; set; }
    public int ProductId { get; set; }
    #endregion
}