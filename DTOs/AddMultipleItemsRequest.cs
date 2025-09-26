namespace ShoppingBasketApi.DTOs;

/// <summary>
/// Request model for adding multiple items to the basket
/// </summary>
public class AddMultipleItemsRequest
{
    /// <summary>
    /// List of items to add to the basket
    /// </summary>
    public List<AddItemRequest> Items { get; set; } = new();
}