namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

internal class DeleteCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
    }
}
