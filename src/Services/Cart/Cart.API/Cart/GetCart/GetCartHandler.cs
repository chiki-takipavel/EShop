namespace Cart.API.Cart.GetCart;

public record GetCartQuery(string UserName) : IQuery<GetCartResult>;

public record GetCartResult(ShoppingCart Cart);

public class GetCartQueryValidator : AbstractValidator<GetCartQuery>
{
    public GetCartQueryValidator()
    {
        RuleFor(q => q.UserName).NotEmpty();
    }
}

public class GetCartQueryHandler : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        return new GetCartResult(new ShoppingCart("default"));
    }
}
