namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteProductCommand(id);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .WithDescription("Delete Product")
        .WithSummary("Delete Product")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
