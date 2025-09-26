using ShoppingBasketApi.Models;

namespace ShoppingBasketApi.DTOs;

/// <summary>
/// Response model containing basket summary and costs
/// </summary>
public class BasketSummaryResponse
{
    /// <summary>
    /// Basket identifier
    /// </summary>
    public string BasketId { get; set; } = string.Empty;
    
    /// <summary>
    /// List of items in the basket
    /// </summary>
    public List<BasketItem> Items { get; set; } = new();
    
    /// <summary>
    /// Applied discount code
    /// </summary>
    public string? DiscountCode { get; set; }
    
    /// <summary>
    /// Discount code percentage
    /// </summary>
    public decimal DiscountCodePercentage { get; set; }
    
    /// <summary>
    /// Shipping destination country
    /// </summary>
    public string ShippingCountry { get; set; } = "UK";
    
    /// <summary>
    /// Subtotal excluding VAT and shipping
    /// </summary>
    public decimal SubtotalExcludingVat { get; set; }
    
    /// <summary>
    /// Shipping cost
    /// </summary>
    public decimal ShippingCost { get; set; }
    
    /// <summary>
    /// VAT amount
    /// </summary>
    public decimal VatAmount { get; set; }
    
    /// <summary>
    /// Total excluding VAT (includes shipping)
    /// </summary>
    public decimal TotalExcludingVat { get; set; }
    
    /// <summary>
    /// Total including VAT and shipping
    /// </summary>
    public decimal TotalIncludingVat { get; set; }
}