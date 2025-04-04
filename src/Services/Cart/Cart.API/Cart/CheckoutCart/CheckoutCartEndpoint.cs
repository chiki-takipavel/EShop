namespace Cart.API.Cart.CheckoutCart;

public record CheckoutCartRequest(CartCheckoutDto CartCheckoutDto);

public record CheckoutCartResponse(bool IsSuccess);

public class CheckoutCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cart/checkout", async (CheckoutCartRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckoutCartCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CheckoutCartResponse>();

            return Results.Ok(response);
        })
        .WithName("CheckoutCart")
        .WithDescription("Checkout Cart")
        .WithSummary("Checkout Cart")
        .Produces<CheckoutCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
