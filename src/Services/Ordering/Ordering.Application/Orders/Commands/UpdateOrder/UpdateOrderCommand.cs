namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

internal class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Order.Id).NotEmpty();
        RuleFor(c => c.Order.OrderName).NotEmpty().WithName("Name");
        RuleFor(c => c.Order.CustomerId).NotEmpty();
    }
}
