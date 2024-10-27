namespace Catalog.API.Exceptions;

internal class ProductNotFoundException : Exception
{
    internal ProductNotFoundException() : base("Product not found!") { }
}
