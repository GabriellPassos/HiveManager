using HiveManager.Data;
using HiveManager.Interface;
using HiveManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<BeeSpecieService>();
builder.Services.AddTransient<DerivativeService>();
builder.Services.AddTransient<WithdrawnHoneyService>();
builder.Services.AddTransient<IHiveService>();
builder.Services.AddTransient<PackageService>();
builder.Services.AddTransient<SaleService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<PriceService>();
builder.Services.AddDbContext<HiveContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'livrariaContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
DatabaseManagerService.MigrationInitialisation(app);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
