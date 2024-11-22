namespace Cart.API.Cart.GetCart;

public record GetCartResponse(ShoppingCart Cart);

public class GetCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cart/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetCartQuery(userName));

            var response = result.Adapt<GetCartResponse>();

            return Results.Ok(response);
        })
        .WithName("GetCart")
        .WithDescription("Get Cart")
        .WithSummary("Get Cart")
        .Produces<GetCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
