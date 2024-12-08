namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupones.AsNoTracking()
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName)
            ?? new Coupon { ProductName = "No Discount", Description = "No Discount", Percentage = 0 };

        logger.LogInformation("Discount is retrieved for ProductName: {ProductName}, Percentage: {Percentage}.",
            coupon.ProductName, coupon.Percentage);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        if (coupon.Percentage is < 1 or > 99)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Percentage must be in range 1..99."));
        }

        dbContext.Coupones.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName: {ProductName}, Percentage: {Percentage}.",
            coupon.ProductName, coupon.Percentage);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        if (coupon.Percentage is < 1 or > 99)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Percentage must be in range 1..99."));
        }

        dbContext.Coupones.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}, Percentage: {Percentage}.",
            coupon.ProductName, coupon.Percentage);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupones
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName: {request.ProductName} is not found."));

        dbContext.Coupones.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully deleted. ProductName: {ProductName}, Percentage: {Percentage}.",
            coupon.ProductName, coupon.Percentage);

        return new DeleteDiscountResponse { IsSuccess = true };
    }
}
