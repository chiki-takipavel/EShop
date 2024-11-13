namespace Catalog.API.Exceptions;

internal class ProductNotFoundException : NotFoundException
{
    internal ProductNotFoundException(Guid id) : base("Product", id) { }
}
