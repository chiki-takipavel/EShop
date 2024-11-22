namespace Cart.API.Cart.DeleteCart;

public record DeleteCartCommand(string UserName) : ICommand<DeleteCartResult>;

public record DeleteCartResult(bool IsSuccess);

public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartValidator()
    {
        RuleFor(c => c.UserName).NotEmpty();
    }
}

public class DeleteCartCommandHandler : ICommandHandler<DeleteCartCommand, DeleteCartResult>
{
    public async Task<DeleteCartResult> Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        return new DeleteCartResult(true);
    }
}
