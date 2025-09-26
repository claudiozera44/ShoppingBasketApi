namespace ShoppingBasketApi.Models;

/// <summary>
/// Represents a shopping basket containing items and associated costs
/// </summary>
public class Basket
{
    /// <summary>
    /// Unique identifier for the basket
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// List of items in the basket
    /// </summary>
    public List<BasketItem> Items { get; set; } = new();
    
    /// <summary>
    /// Applied discount code (if any)
    /// </summary>
    public string? DiscountCode { get; set; }
    
    /// <summary>
    /// Discount percentage from the discount code
    /// </summary>
    public decimal DiscountCodePercentage { get; set; } = 0;
    
    /// <summary>
    /// Shipping destination country
    /// </summary>
    public string ShippingCountry { get; set; } = "UK";
    
    /// <summary>
    /// VAT rate (20% for UK)
    /// </summary>
    public const decimal VatRate = 0.20m;
    
    /// <summary>
    /// UK shipping cost
    /// </summary>
    public const decimal UkShippingCost = 5.99m;
    
    /// <summary>
    /// International shipping cost
    /// </summary>
    public const decimal InternationalShippingCost = 15.99m;
    
    /// <summary>
    /// Calculates subtotal excluding VAT and shipping
    /// </summary>
    public decimal SubtotalExcludingVat
    {
        get
        {
            var itemsTotal = Items.Sum(item => item.TotalPriceWithDiscount);
            
            // Apply discount code to non-discounted items only
            var nonDiscountedItemsTotal = Items
                .Where(item => !item.IsDiscounted)
                .Sum(item => item.TotalPrice);
            
            var discountedItemsTotal = Items
                .Where(item => item.IsDiscounted)
                .Sum(item => item.TotalPriceWithDiscount);
            
            var discountCodeReduction = nonDiscountedItemsTotal * (DiscountCodePercentage / 100);
            
            return (nonDiscountedItemsTotal - discountCodeReduction) + discountedItemsTotal;
        }
    }
    
    /// <summary>
    /// Calculates shipping cost based on destination
    /// </summary>
    public decimal ShippingCost => ShippingCountry.ToUpper() == "UK" ? UkShippingCost : InternationalShippingCost;
    
    /// <summary>
    /// Calculates VAT amount
    /// </summary>
    public decimal VatAmount => SubtotalExcludingVat * VatRate;
    
    /// <summary>
    /// Calculates total including VAT and shipping
    /// </summary>
    public decimal TotalIncludingVat => SubtotalExcludingVat + VatAmount + ShippingCost;
    
    /// <summary>
    /// Calculates total excluding VAT but including shipping
    /// </summary>
    public decimal TotalExcludingVat => SubtotalExcludingVat + ShippingCost;
}