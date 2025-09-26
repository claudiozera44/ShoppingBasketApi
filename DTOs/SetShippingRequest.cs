using System.ComponentModel.DataAnnotations;

namespace ShoppingBasketApi.DTOs;

/// <summary>
/// Request model for setting shipping destination
/// </summary>
public class SetShippingRequest
{
    /// <summary>
    /// Destination country code (e.g., "UK", "US", "DE")
    /// </summary>
    [Required]
    public string Country { get; set; } = "UK";
}