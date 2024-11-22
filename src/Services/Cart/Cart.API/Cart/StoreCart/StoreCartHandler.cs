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

public class StoreCartHandler : ICommandHandler<StoreCartCommand, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        return new StoreCartResult("default");
    }
}
