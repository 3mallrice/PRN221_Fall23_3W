using DAO;
using Repositories;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ILotRepo, LotRepo>();
builder.Services.AddScoped<ILotService, LotService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockOutService, StockOutService>();
builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
builder.Services.AddScoped<IPartnerService, PartnerService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseSession();

app.Run();
