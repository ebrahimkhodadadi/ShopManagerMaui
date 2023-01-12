
using Entities;

namespace Shared.Models;

public class BasketDto
{
    public int Id { get; set; }
    public int Count { get; set; }
    public int UserId { get; set; }
    public int StoreProductID { get; set; }
}

public class BasketListDto
{
    public int Id { get; set; }
    public int BasketCount { get; set; }
    public int UserId { get; set; }

    public int ProductId { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    public int StoreID { get; set; }

    public int StoreProductID { get; set; }
    public int StoredProductCount { get; set; }
}

public class TransferBasketDto
{
    public int UserId { get; set; }
    public string? Description { get; set; }
    public int DestinaionStoreID { get; set; }
    public string? DestinaionStoreName { get; set; }
}