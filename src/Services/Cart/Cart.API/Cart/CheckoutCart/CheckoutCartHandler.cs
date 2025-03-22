namespace Cart.API.Cart.CheckoutCart;

public record CheckoutCartCommand(CartCheckoutDto CartCheckoutDto) : ICommand<CheckoutCartResult>;

public record CheckoutCartResult(bool IsSuccess);

public class CheckoutCartCommandValidator : AbstractValidator<CheckoutCartCommand>
{
    public CheckoutCartCommandValidator()
    {
        RuleFor(c => c.CartCheckoutDto).NotNull();
        RuleFor(c => c.CartCheckoutDto.UserName).NotEmpty();
    }
}

public class CheckoutCartHandler(ICartRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutCartCommand, CheckoutCartResult>
{
    public async Task<CheckoutCartResult> Handle(CheckoutCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await repository.GetCart(command.CartCheckoutDto.UserName, cancellationToken);
        if (cart is null)
        {
            return new CheckoutCartResult(false);
        }

        var eventMessage = command.CartCheckoutDto.Adapt<CartCheckoutEvent>();
        eventMessage.TotalPrice = cart.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteCart(command.CartCheckoutDto.UserName, cancellationToken);

        return new CheckoutCartResult(true);
    }
}
