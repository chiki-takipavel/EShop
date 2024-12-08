namespace Cart.API.Cart.StoreCart;

public record StoreCartCommand(ShoppingCart Cart) : ICommand<StoreCartResult>;

public record StoreCartResult(string UserName);

public class StoreCartCommandValidator : AbstractValidator<StoreCartCommand>
{
    public StoreCartCommandValidator()
    {
        RuleFor(c => c.Cart).NotNull();
        RuleFor(c => c.Cart.UserName).NotEmpty();
    }
}

public class StoreCartHandler(ICartRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreCartCommand, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
    {
        await ApplyDiscount(command.Cart, cancellationToken);

        await repository.StoreCart(command.Cart, cancellationToken);

        return new StoreCartResult(command.Cart.UserName);
    }

    private async Task ApplyDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(
                new GetDiscountRequest { ProductName = item.ProductName },
                cancellationToken: cancellationToken);
            item.Price *= 1m - (coupon.Percentage / 100m);
        }
    }
}
