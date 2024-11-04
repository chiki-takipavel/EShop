namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, string ImageFile,
    decimal Price, List<string> Categories) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().Length(2, 150);
        RuleFor(c => c.Price).GreaterThan(decimal.Zero);
    }
}

internal class CreateProductCommandHandler
    (IDocumentSession session, ILogger<CreateProductCommandHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);

        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
            Categories = command.Categories
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
