using ShoppingBasketApi.Models;
using ShoppingBasketApi.DTOs;
using System.Collections.Concurrent;

namespace ShoppingBasketApi.Services;

/// <summary>
/// In-memory implementation of basket service
/// </summary>
public class BasketService : IBasketService
{
    private readonly ConcurrentDictionary<string, Basket> _baskets = new();
    private readonly List<DiscountCode> _discountCodes;
    
    public BasketService()
    {
        // Initialize with some sample discount codes
        _discountCodes = new List<DiscountCode>
        {
            new DiscountCode { Code = "SAVE10", Percentage = 10, Description = "10% off your order" },
            new DiscountCode { Code = "WELCOME20", Percentage = 20, Description = "20% off for new customers" },
            new DiscountCode { Code = "SUMMER15", Percentage = 15, Description = "15% summer discount" }
        };
    }
    
    public async Task<Basket> GetBasketAsync(string basketId)
    {
        await Task.CompletedTask; // Simulate async operation
        return _baskets.GetOrAdd(basketId, _ => new Basket { Id = basketId });
    }
    
    public async Task<Basket> AddItemAsync(string basketId, AddItemRequest request)
    {
        var basket = await GetBasketAsync(basketId);
        
        // Check if item already exists in basket
        var existingItem = basket.Items.FirstOrDefault(x => x.Id == request.Id);
        if (existingItem != null)
        {
            // Update quantity and properties
            existingItem.Quantity += request.Quantity;
            existingItem.Name = request.Name;
            existingItem.UnitPrice = request.UnitPrice;
            existingItem.IsDiscounted = request.IsDiscounted;
            existingItem.DiscountPercentage = request.DiscountPercentage;
        }
        else
        {
            // Add new item
            var newItem = new BasketItem
            {
                Id = request.Id,
                Name = request.Name,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity,
                IsDiscounted = request.IsDiscounted,
                DiscountPercentage = request.DiscountPercentage
            };
            basket.Items.Add(newItem);
        }
        
        return basket;
    }
    
    public async Task<Basket> AddMultipleItemsAsync(string basketId, AddMultipleItemsRequest request)
    {
        var basket = await GetBasketAsync(basketId);
        
        foreach (var itemRequest in request.Items)
        {
            await AddItemAsync(basketId, itemRequest);
        }
        
        return basket;
    }
    
    public async Task<Basket> RemoveItemAsync(string basketId, string itemId)
    {
        var basket = await GetBasketAsync(basketId);
        
        var itemToRemove = basket.Items.FirstOrDefault(x => x.Id == itemId);
        if (itemToRemove != null)
        {
            basket.Items.Remove(itemToRemove);
        }
        
        return basket;
    }
    
    public async Task<Basket> ApplyDiscountCodeAsync(string basketId, string discountCode)
    {
        var basket = await GetBasketAsync(basketId);
        
        var validDiscountCode = _discountCodes
            .FirstOrDefault(dc => dc.Code.Equals(discountCode, StringComparison.OrdinalIgnoreCase) && dc.IsActive);
        
        if (validDiscountCode != null)
        {
            basket.DiscountCode = validDiscountCode.Code;
            basket.DiscountCodePercentage = validDiscountCode.Percentage;
        }
        else
        {
            throw new ArgumentException("Invalid or inactive discount code", nameof(discountCode));
        }
        
        return basket;
    }
    
    public async Task<Basket> SetShippingDestinationAsync(string basketId, string country)
    {
        var basket = await GetBasketAsync(basketId);
        basket.ShippingCountry = country;
        return basket;
    }
    
    public async Task<List<DiscountCode>> GetAvailableDiscountCodesAsync()
    {
        await Task.CompletedTask; // Simulate async operation
        return _discountCodes.Where(dc => dc.IsActive).ToList();
    }
}