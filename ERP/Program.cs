using ERP.Bll.Commercial.Customer;
using ERP.Bll.Commercial.Supplier;
using ERP.Bll.Company.Branch;
using ERP.Bll.Company.Company;
using ERP.Bll.Company.Point;
using ERP.Bll.Company.Warehouse;
using ERP.Bll.Invoice.MovementInvoice;
using ERP.Bll.Location;
using ERP.Bll.Pay;
using ERP.Bll.Products.Brand;
using ERP.Bll.Products.Category;
using ERP.Bll.Products.Industry;
using ERP.Bll.Products.Product;
using ERP.Bll.Products.Stock;
using ERP.Bll.Products.Tax;
using ERP.Bll.Security.Authentication;
using ERP.Bll.Security.Module;
using ERP.Bll.Security.Option;
using ERP.Bll.Security.Profile;
using ERP.Bll.Security.Role;
using ERP.Bll.Security.User;
using ERP.CoreDB;
using ERP.Filters;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Interface DB
builder.Services.AddDbContext<BaseErpContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionDB").Get<string>());
});
// :: Interfaces - aplicativo (inicio)
// Bll
builder.Services.AddScoped<IAuthenticationBll, AuthenticationBll>();
builder.Services.AddScoped<IProfileBll, ProfileBll>();
builder.Services.AddScoped<IUserBll, UserBll>();
builder.Services.AddScoped<IUserRoleBll, UserRoleBll>();
builder.Services.AddScoped<IRoleBll, RoleBll>();
builder.Services.AddScoped<IUserPermissionBll, UserPermissionBll>();

builder.Services.AddScoped<ICompanyBll, CompanyBll>();
builder.Services.AddScoped<IBranchBll, BranchBll>();
builder.Services.AddScoped<IPointBll, PointBll>();
builder.Services.AddScoped<IWarehouseBll, WarehouseBll>();

builder.Services.AddScoped<ICustomerBll, CustomerBll>();
builder.Services.AddScoped<ISupplierBll, SupplierBll>();
builder.Services.AddScoped<IMovementInvoiceBll, MovementInvoiceBll>();

builder.Services.AddScoped<ILocationBll, LocationBll>();
builder.Services.AddScoped<IFormPayBll, FormPayBll>();

builder.Services.AddScoped<IBrandBll, BrandBll>();
builder.Services.AddScoped<ICategoryBll, CategoryBll>();
builder.Services.AddScoped<IIndustryBll, IndustryBll>();
builder.Services.AddScoped<ITaxBll, TaxBll>();
builder.Services.AddScoped<IProductBll, ProductBll>();
builder.Services.AddScoped<IStockBll, StockBll>();
builder.Services.AddScoped<IOptionBll, OptionBll>();
builder.Services.AddScoped<IModuleBll, ModuleBll>();


// :: Interfaces - aplicativo (fin)

builder.Services.AddHttpContextAccessor();

// Filter
builder.Services.AddScoped<SessionUserFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
