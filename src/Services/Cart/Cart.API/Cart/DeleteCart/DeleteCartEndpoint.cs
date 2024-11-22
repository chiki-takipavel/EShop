namespace Cart.API.Cart.DeleteCart;

public record DeleteCartResponse(bool IsSuccess);

public class GetCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/cart/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCartCommand(userName));

            var response = result.Adapt<DeleteCartResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteCart")
        .WithDescription("Delete Cart")
        .WithSummary("Delete Cart")
        .Produces<DeleteCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
