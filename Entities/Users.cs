using Entities.Common;

namespace Entities;
public class Users : BaseEntity
{
    public string Name { get; set; }


    #region Foreign Key
    public ICollection<TransportLogger> TransportLogger { get; set; }
    #endregion
}
