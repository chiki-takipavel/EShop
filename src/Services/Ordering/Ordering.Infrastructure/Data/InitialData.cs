namespace Ordering.Infrastructure.Data;

internal static class InitialData
{
    public static IEnumerable<Customer> Customers =>
    [
        Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "Pavel", "pavel@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "John", "john@gmail.com")
    ];

    public static IEnumerable<Product> Products =>
    [
        Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "IPhone X", 500),
        Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Samsung 10", 400),
        Product.Create(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Huawei Plus", 650),
        Product.Create(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Xiaomi Mi", 450)
    ];

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var firstAddress = Address.Of("Pavel", "Miskevich", "pavel@gmail.com", "Nyamiha", "Belarus", "Minsk", "220000");
            var secondAddress = Address.Of("John", "Doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

            var firstPayment = Payment.Of("Pavel", "5555555555554444", "12/28", "355", 1);
            var secondPayment = Payment.Of("John", "8885555555554444", "06/30", "222", 2);

            var firstOrder = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                OrderName.Of("ORD_1"),
                shippingAddress: firstAddress,
                billingAddress: firstAddress,
                firstPayment);
            firstOrder.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 2, 500);
            firstOrder.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 1, 400);

            var secondOrder = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
                OrderName.Of("ORD_2"),
                shippingAddress: secondAddress,
                billingAddress: secondAddress,
                secondPayment);
            secondOrder.Add(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 1, 650);
            secondOrder.Add(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 2, 450);

            return [firstOrder, secondOrder];
        }
    }
}
