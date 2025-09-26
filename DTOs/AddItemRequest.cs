using System.ComponentModel.DataAnnotations;

namespace ShoppingBasketApi.DTOs;

/// <summary>
/// Request model for adding an item to the basket
/// </summary>
public class AddItemRequest
{
    /// <summary>
    /// Unique identifier for the item
    /// </summary>
    [Required]
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Name of the item
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Unit price of the item
    /// </summary>
    [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
    public decimal UnitPrice { get; set; }
    
    /// <summary>
    /// Quantity to add (default: 1)
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; } = 1;
    
    /// <summary>
    /// Whether this is a discounted item
    /// </summary>
    public bool IsDiscounted { get; set; } = false;
    
    /// <summary>
    /// Discount percentage if discounted (0-100)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100")]
    public decimal DiscountPercentage { get; set; } = 0;
}