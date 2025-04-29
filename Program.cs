using DatabaseLayer.ApplicationContext;
using DatabaseLayer.DBOperation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDBContext>(o=> o.UseSqlServer("Data Source=.;Initial Catalog=ECommerceDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

builder.Services.AddScoped<ManageCategory>();
builder.Services.AddScoped<ManageProduct>();
builder.Services.AddScoped<ManageProductvarient>();
builder.Services.AddScoped<ManageStock>();
builder.Services.AddScoped<ManageRegistration>();
builder.Services.AddScoped<ManageCustomerAddress>();
builder.Services.AddScoped<ManageCart>();
builder.Services.AddScoped<ManageOrder>();
builder.Services.AddScoped<ManageCourier>();
builder.Services.AddScoped<ManageProduct>();
builder.Services.AddScoped<ManageShipping>();
builder.Services.AddScoped<ManageDelivredOrder>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
