namespace Cart.API.Repositories;

public interface ICartRepository
{
    Task<ShoppingCart> GetCart(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreCart(ShoppingCart cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteCart(string userName, CancellationToken cancellationToken = default);
}
