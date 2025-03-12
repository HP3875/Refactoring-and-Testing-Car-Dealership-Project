using CarDealership.Data;
using Microsoft.EntityFrameworkCore;
using CarDealership.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Register the DbContext with the DI container
builder.Services.AddDbContext<CarDealershipContext>(options =>
    options.UseSqlite("Data Source=CarDealership.db"));

// Register the CarService with the DI container
builder.Services.AddScoped<CarService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
