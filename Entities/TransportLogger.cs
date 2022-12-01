
namespace Entities;

public class TransportLogger : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ProductList { get; set; }
    public int Count { get; set; }
    
    #region Foreign Key
    public Stores SourceStore { get; set; }
    public int SourceStoreId { get; set; }  
    
    public Stores DestinationStore { get; set; }
    public int DestinationStoreId { get; set; }

    public Users TransPorter { get; set; }
    public int TransPorterId { get; set; }
    #endregion
}
