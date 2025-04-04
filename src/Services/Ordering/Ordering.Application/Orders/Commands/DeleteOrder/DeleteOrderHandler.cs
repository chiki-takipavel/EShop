namespace Ordering.Application.Orders.Commands.DeleteOrder;

internal class DeleteOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        OrderId orderId = OrderId.Of(command.OrderId);
        Order order = await dbContext.Orders.FindAsync([orderId], cancellationToken)
            ?? throw new OrderNotFoundException(command.OrderId);

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}
