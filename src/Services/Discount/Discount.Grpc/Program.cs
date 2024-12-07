var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DiscountContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"))
);

builder.Services.AddGrpc();

var app = builder.Build();

app.UseMigration();
app.MapGrpcService<DiscountService>();

await app.RunAsync();
