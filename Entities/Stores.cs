namespace Entities;

public class Stores : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    #region Foreign Key
    public ICollection<StoredProducts> StoredProducts { get; set; }
    #endregion
}
