namespace Cart.API.Domain;

public class ShoppingCartItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public int Quantity { get; set; }
}
