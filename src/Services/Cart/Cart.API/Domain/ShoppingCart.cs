﻿namespace Cart.API.Domain;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

    public ShoppingCart() { }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
}
