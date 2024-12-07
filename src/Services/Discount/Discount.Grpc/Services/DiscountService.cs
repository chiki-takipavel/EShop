namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupones.AsNoTracking()
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName)
            ?? new Coupon { ProductName = "No Discount", Description = "No Discount", Amount = 0 };

        logger.LogInformation("Discount is retrieved for ProductName: {ProductName}, Amount: {Amount}.",
            coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.Coupones.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName: {ProductName}, Amount: {Amount}.",
            coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.Coupones.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}, Amount: {Amount}.",
            coupon.ProductName, coupon.Amount);

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

        logger.LogInformation("Discount is successfully deleted. ProductName: {ProductName}, Amount: {Amount}.",
            coupon.ProductName, coupon.Amount);

        return new DeleteDiscountResponse { IsSuccess = true };
    }
}
