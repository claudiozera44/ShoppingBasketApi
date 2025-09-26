namespace ShoppingBasketApi.Models;

/// <summary>
/// Represents an item in the shopping basket
/// </summary>
public class BasketItem
{
    /// <summary>
    /// Unique identifier for the item
    /// </summary>
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Name of the item
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Unit price of the item (before any discounts)
    /// </summary>
    public decimal UnitPrice { get; set; }
    
    /// <summary>
    /// Quantity of this item in the basket
    /// </summary>
    public int Quantity { get; set; } = 1;
    
    /// <summary>
    /// Indicates if this item has a discount applied
    /// </summary>
    public bool IsDiscounted { get; set; } = false;
    
    /// <summary>
    /// Discount percentage (0-100) if the item is discounted
    /// </summary>
    public decimal DiscountPercentage { get; set; } = 0;
    
    /// <summary>
    /// Calculates the total price for this item (unit price * quantity)
    /// </summary>
    public decimal TotalPrice => UnitPrice * Quantity;
    
    /// <summary>
    /// Calculates the total price after discount
    /// </summary>
    public decimal TotalPriceWithDiscount => IsDiscounted 
        ? TotalPrice * (1 - DiscountPercentage / 100) 
        : TotalPrice;
}