using ShoppingBasketApi.Models;
using ShoppingBasketApi.DTOs;

namespace ShoppingBasketApi.Services;

/// <summary>
/// Interface for basket service operations
/// </summary>
public interface IBasketService
{
    /// <summary>
    /// Gets a basket by ID, creates new one if not found
    /// </summary>
    Task<Basket> GetBasketAsync(string basketId);
    
    /// <summary>
    /// Adds a single item to the basket
    /// </summary>
    Task<Basket> AddItemAsync(string basketId, AddItemRequest request);
    
    /// <summary>
    /// Adds multiple items to the basket
    /// </summary>
    Task<Basket> AddMultipleItemsAsync(string basketId, AddMultipleItemsRequest request);
    
    /// <summary>
    /// Removes an item from the basket
    /// </summary>
    Task<Basket> RemoveItemAsync(string basketId, string itemId);
    
    /// <summary>
    /// Applies a discount code to the basket
    /// </summary>
    Task<Basket> ApplyDiscountCodeAsync(string basketId, string discountCode);
    
    /// <summary>
    /// Sets the shipping destination for the basket
    /// </summary>
    Task<Basket> SetShippingDestinationAsync(string basketId, string country);
    
    /// <summary>
    /// Gets all available discount codes
    /// </summary>
    Task<List<DiscountCode>> GetAvailableDiscountCodesAsync();
}