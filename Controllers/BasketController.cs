using Microsoft.AspNetCore.Mvc;
using ShoppingBasketApi.Services;
using ShoppingBasketApi.DTOs;
using ShoppingBasketApi.Models;

namespace ShoppingBasketApi.Controllers;

/// <summary>
/// Controller for managing shopping baskets
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    
    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }
    
    /// <summary>
    /// Gets a basket by ID with cost breakdown
    /// </summary>
    /// <param name="basketId">Unique basket identifier</param>
    /// <returns>Basket summary with all costs</returns>
    [HttpGet("{basketId}")]
    public async Task<ActionResult<BasketSummaryResponse>> GetBasket(string basketId)
    {
        var basket = await _basketService.GetBasketAsync(basketId);
        return Ok(MapToSummaryResponse(basket));
    }
    
    /// <summary>
    /// Adds a single item to the basket
    /// </summary>
    /// <param name="basketId">Unique basket identifier</param>
    /// <param name="request">Item details to add</param>
    /// <returns>Updated basket summary</returns>
    [HttpPost("{basketId}/items")]
    public async Task<ActionResult<BasketSummaryResponse>> AddItem(string basketId, AddItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        var basket = await _basketService.AddItemAsync(basketId, request);
        return Ok(MapToSummaryResponse(basket));
    }
    
    /// <summary>
    /// Adds multiple items to the basket
    /// </summary>
    /// <param name="basketId">Unique basket identifier</param>
    /// <param name="request">List of items to add</param>
    /// <returns>Updated basket summary</returns>
    [HttpPost("{basketId}/items/bulk")]
    public async Task<ActionResult<BasketSummaryResponse>> AddMultipleItems(string basketId, AddMultipleItemsRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        var basket = await _basketService.AddMultipleItemsAsync(basketId, request);
        return Ok(MapToSummaryResponse(basket));
    }
    
    /// <summary>
    /// Removes an item from the basket
    /// </summary>
    /// <param name="basketId">Unique basket identifier</param>
    /// <param name="itemId">Item identifier to remove</param>
    /// <returns>Updated basket summary</returns>
    [HttpDelete("{basketId}/items/{itemId}")]
    public async Task<ActionResult<BasketSummaryResponse>> RemoveItem(string basketId, string itemId)
    {
        var basket = await _basketService.RemoveItemAsync(basketId, itemId);
        return Ok(MapToSummaryResponse(basket));
    }
    
    /// <summary>
    /// Gets the total cost including VAT and shipping
    /// </summary>
    /// <param name="basketId">Unique basket identifier</param>
    /// <returns>Total cost including VAT</returns>
    [HttpGet("{basketId}/total-with-vat")]
    public async Task<ActionResult<decimal>> GetTotalWithVat(string basketId)
    {
        var basket = await _basketService.GetBasketAsync(basketId);
        return Ok(Math.Round(basket.TotalIncludingVat, 2));
    }
    
    /// <summary>
    /// Gets the total cost excluding VAT (but including shipping)
    /// </summary>
    /// <param name="basketId">Unique basket identifier</param>
    /// <returns>Total cost excluding VAT</returns>
    [HttpGet("{basketId}/total-without-vat")]
    public async Task<ActionResult<decimal>> GetTotalWithoutVat(string basketId)
    {
        var basket = await _basketService.GetBasketAsync(basketId);
        return Ok(Math.Round(basket.TotalExcludingVat, 2));
    }
    
    /// <summary>
    /// Applies a discount code to the basket
    /// </summary>
    /// <param name="basketId">Unique basket identifier</param>
    /// <param name="request">Discount code to apply</param>
    /// <returns>Updated basket summary</returns>
    [HttpPost("{basketId}/discount-code")]
    public async Task<ActionResult<BasketSummaryResponse>> ApplyDiscountCode(string basketId, ApplyDiscountCodeRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        try
        {
            var basket = await _basketService.ApplyDiscountCodeAsync(basketId, request.DiscountCode);
            return Ok(MapToSummaryResponse(basket));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Sets the shipping destination for the basket
    /// </summary>
    /// <param name="basketId">Unique basket identifier</param>
    /// <param name="request">Shipping destination details</param>
    /// <returns>Updated basket summary</returns>
    [HttpPost("{basketId}/shipping")]
    public async Task<ActionResult<BasketSummaryResponse>> SetShipping(string basketId, SetShippingRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        var basket = await _basketService.SetShippingDestinationAsync(basketId, request.Country);
        return Ok(MapToSummaryResponse(basket));
    }
    
    /// <summary>
    /// Gets all available discount codes
    /// </summary>
    /// <returns>List of available discount codes</returns>
    [HttpGet("discount-codes")]
    public async Task<ActionResult<List<DiscountCode>>> GetDiscountCodes()
    {
        var discountCodes = await _basketService.GetAvailableDiscountCodesAsync();
        return Ok(discountCodes);
    }
    
    private static BasketSummaryResponse MapToSummaryResponse(Basket basket)
    {
        return new BasketSummaryResponse
        {
            BasketId = basket.Id,
            Items = basket.Items,
            DiscountCode = basket.DiscountCode,
            DiscountCodePercentage = basket.DiscountCodePercentage,
            ShippingCountry = basket.ShippingCountry,
            SubtotalExcludingVat = Math.Round(basket.SubtotalExcludingVat, 2),
            ShippingCost = Math.Round(basket.ShippingCost, 2),
            VatAmount = Math.Round(basket.VatAmount, 2),
            TotalExcludingVat = Math.Round(basket.TotalExcludingVat, 2),
            TotalIncludingVat = Math.Round(basket.TotalIncludingVat, 2)
        };
    }
}