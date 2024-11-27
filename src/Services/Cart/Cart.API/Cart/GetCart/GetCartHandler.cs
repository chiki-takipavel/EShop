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

public class GetCartQueryHandler(ICartRepository repository)
    : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        ShoppingCart cart = await repository.GetCart(query.UserName, cancellationToken);

        return new GetCartResult(cart);
    }
}
