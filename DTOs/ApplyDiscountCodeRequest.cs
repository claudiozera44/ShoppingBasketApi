using System.ComponentModel.DataAnnotations;

namespace ShoppingBasketApi.DTOs;

/// <summary>
/// Request model for applying a discount code
/// </summary>
public class ApplyDiscountCodeRequest
{
    /// <summary>
    /// The discount code to apply
    /// </summary>
    [Required]
    public string DiscountCode { get; set; } = string.Empty;
}