namespace Ordering.Application.Exceptions;

internal class OrderNotFoundException(Guid id) : NotFoundException("Order", id) { }
