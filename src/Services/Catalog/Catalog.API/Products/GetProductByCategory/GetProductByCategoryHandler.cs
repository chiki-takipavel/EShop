namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IReadOnlyCollection<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Categories.Any(c => c.Equals(query.Category, StringComparison.OrdinalIgnoreCase)))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
