# Shopping Basket API

A comprehensive .NET 8 Web API for managing online shopping baskets with VAT calculations, discount handling, and shipping cost management.

## Features

- **Item Management**: Add single items, multiple items, or multiple quantities of the same item
- **Remove Items**: Remove items from the basket using REST DELETE operations
- **VAT Calculations**: Automatic 20% VAT calculation for UK customers
- **Discount System**: 
  - Individual item discounts (applied at item level)
  - Discount codes that apply to non-discounted items only
- **Shipping Costs**: 
  - UK shipping: £5.99
  - International shipping: £15.99
- **Cost Breakdown**: Get totals with and without VAT
- **In-Memory Storage**: Fast, thread-safe in-memory data storage
- **Swagger Documentation**: Complete API documentation with interactive testing

## Technology Stack

- **.NET 8.0**: Latest LTS version
- **ASP.NET Core Web API**: RESTful API framework
- **Swagger/OpenAPI**: API documentation and testing
- **In-Memory Storage**: Thread-safe concurrent collections
- **XML Documentation**: Comprehensive code documentation

## Project Structure

```
ShoppingBasketApi/
├── Controllers/
│   └── BasketController.cs          # REST API endpoints
├── Services/
│   ├── IBasketService.cs           # Service interface
│   └── BasketService.cs            # Business logic implementation
├── Models/
│   ├── Basket.cs                   # Main basket model
│   ├── BasketItem.cs               # Individual item model
│   └── DiscountCode.cs             # Discount code model
├── DTOs/
│   ├── AddItemRequest.cs           # Add item request model
│   ├── AddMultipleItemsRequest.cs  # Bulk add request model
│   ├── ApplyDiscountCodeRequest.cs # Discount code request model
│   ├── SetShippingRequest.cs       # Shipping request model
│   └── BasketSummaryResponse.cs    # Complete basket response model
├── Program.cs                       # Application entry point
├── ShoppingBasketAPI.postman_collection.json # Postman test collection
└── README.md                       # This documentation
```

## Setup and Installation

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio Code (recommended) or any text editor
- Postman (optional, for testing)

### Running the Application

1. **Clone or download the project**
2. **Navigate to the project directory**:
   ```bash
   cd ShoppingBasketApi
   ```

3. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

4. **Build the project**:
   ```bash
   dotnet build
   ```

5. **Run the application**:
   ```bash
   dotnet run
   ```

6. **Access the API**:
   - API Base URL: `http://localhost:5000`
   - Swagger UI: `http://localhost:5000` (automatically redirects to Swagger)
   - API Documentation: `http://localhost:5000/swagger`

### Development in VS Code

1. Open the `ShoppingBasketApi` folder in VS Code
2. Install the C# extension if not already installed
3. Press `F5` to run with debugging, or use `Ctrl+F5` to run without debugging
4. The API will start automatically and open Swagger UI

## API Endpoints

### Basket Management
- `GET /api/basket/{basketId}` - Get basket with cost breakdown
- `POST /api/basket/{basketId}/items` - Add single item
- `POST /api/basket/{basketId}/items/bulk` - Add multiple items
- `DELETE /api/basket/{basketId}/items/{itemId}` - Remove item

### Cost Calculations
- `GET /api/basket/{basketId}/total-with-vat` - Get total including VAT
- `GET /api/basket/{basketId}/total-without-vat` - Get total excluding VAT

### Discounts and Shipping
- `POST /api/basket/{basketId}/discount-code` - Apply discount code
- `POST /api/basket/{basketId}/shipping` - Set shipping destination
- `GET /api/basket/discount-codes` - Get available discount codes

## Testing with Postman

1. Import the `ShoppingBasketAPI.postman_collection.json` file into Postman
2. The collection includes:
   - Environment variables (`base_url`, `basket_id`)
   - Sample requests for all endpoints
   - Test data demonstrating all features

### Available Discount Codes
- `SAVE10` - 10% off your order
- `WELCOME20` - 20% off for new customers
- `SUMMER15` - 15% summer discount

## Usage Examples

### Adding Items
```json
POST /api/basket/my-basket/items
{
  "id": "coffee-001",
  "name": "Premium Coffee Beans",
  "unitPrice": 15.99,
  "quantity": 2,
  "isDiscounted": false,
  "discountPercentage": 0
}
```

### Adding Discounted Item
```json
POST /api/basket/my-basket/items
{
  "id": "tea-001",
  "name": "Organic Tea Collection",
  "unitPrice": 24.99,
  "quantity": 1,
  "isDiscounted": true,
  "discountPercentage": 25
}
```

### Applying Discount Code
```json
POST /api/basket/my-basket/discount-code
{
  "discountCode": "SAVE10"
}
```

### Setting Shipping
```json
POST /api/basket/my-basket/shipping
{
  "country": "UK"
}
```

## Cost Calculation Logic

1. **Item Pricing**: Unit price × quantity
2. **Item Discounts**: Applied individually to discounted items
3. **Discount Codes**: Applied only to non-discounted items
4. **Shipping**: £5.99 for UK, £15.99 for international
5. **VAT**: 20% applied to subtotal (before shipping)
6. **Final Total**: (Subtotal + VAT + Shipping)

## Git Repository Setup

The project is ready for Git version control:

```bash
git init
git add .
git commit -m "Initial commit: Shopping Basket API with .NET 8"
git remote add origin [your-repo-url]
git push -u origin main
```

## Development Notes

- **Thread Safety**: Uses `ConcurrentDictionary` for thread-safe in-memory storage
- **Validation**: Comprehensive input validation with proper error responses
- **Documentation**: XML comments throughout for IntelliSense and Swagger
- **Error Handling**: Graceful error handling with appropriate HTTP status codes
- **RESTful Design**: Follows REST principles with proper HTTP methods and status codes

## Future Enhancements

- Database persistence (Entity Framework Core)
- User authentication and basket ownership
- Inventory management and stock validation
- Payment integration
- Order history and tracking
- Advanced discount rules (minimum order value, category-specific)
- Rate limiting and API versioning