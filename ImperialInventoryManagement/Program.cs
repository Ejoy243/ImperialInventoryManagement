using ImperialInventoryManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ImperialInventoryManagement.Models;
using ImperialInventoryManagement.Repos;
using ImperialInventoryManagement.Services;
using Serilog.Events;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

string logPath = builder.Environment.ContentRootPath + "/" +
    builder.Environment.ApplicationName;

builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Async(a => a.Console())
            .WriteTo.MSSqlServer(
                connectionString: "Server=localhost,1433;Database=ImperialData;User Id=sa;Password=password;TrustServerCertificate=True;",
                sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
                restrictedToMinimumLevel: LogEventLevel.Information
)
);



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
options.SignIn.RequireConfirmedAccount = !builder.Environment.IsDevelopment())
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
//Repos
builder.Services.AddScoped<IRepo<Item>, ItemRepo>();
builder.Services.AddScoped<IRepo<InventoryItem>, InventoryItemRepo>();
builder.Services.AddScoped<IRepo<Facility>, FacilityRepo>();
builder.Services.AddScoped<IRepo<Order>, OrderRepo>();
builder.Services.AddScoped<IRepo<Shipment>, ShipmentRepo>();
builder.Services.AddScoped<IRepo<Category>, CategoryRepo>();
builder.Services.AddScoped<IRepo<ItemCategory>, ItemCategoryRepo>();
//Services
builder.Services.AddScoped<FacilityService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ShipmentService>();
builder.Services.AddScoped<InventoryItemService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ItemCategoryService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ShipmentService>();

builder.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always; // Forces Secure flag
});

var app = builder.Build();



app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    await next();
});




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}





app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
