using Microsoft.EntityFrameworkCore;
using Product_Inventory_Management_System.Interfaces;
using Product_Inventory_Management_System.Models;
using Product_Inventory_Management_System.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddControllersWithViews();
builder.Services.AddLogging(builder => builder.AddConsole());
var connectionString = builder.Configuration.GetConnectionString("SQLServerIdentityConnection") ?? throw new InvalidOperationException("Connection string 'SQLServerIdentityConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
    );
builder.Services.AddTransient<IProduct_CRUD, Product_CRUD>();
var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();