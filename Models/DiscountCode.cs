namespace ShoppingBasketApi.Models;

/// <summary>
/// Represents a discount code that can be applied to the basket
/// </summary>
public class DiscountCode
{
    /// <summary>
    /// The discount code string
    /// </summary>
    public string Code { get; set; } = string.Empty;
    
    /// <summary>
    /// Discount percentage (0-100)
    /// </summary>
    public decimal Percentage { get; set; }
    
    /// <summary>
    /// Whether the code is currently active
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Description of the discount
    /// </summary>
    public string Description { get; set; } = string.Empty;
}