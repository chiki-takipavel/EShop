namespace Cart.API.Domain;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public decimal TotalPrice { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = [];

    public ShoppingCart() { }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
}
