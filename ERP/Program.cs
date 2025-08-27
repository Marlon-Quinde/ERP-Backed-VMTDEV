<<<<<<< Updated upstream
using ERP.Bll.Invoice;
=======
using ERP.Bll.Empresa;
using ERP.Bll.Location;
using ERP.Bll.PointOfIssueBll;
using ERP.Bll.PointSaleBll;
using ERP.Bll.PuntoVenta;
using ERP.Bll.Role;
>>>>>>> Stashed changes
using ERP.Bll.Security.Authentication;
using ERP.Bll.Security.Profile;
using ERP.Bll.User;
using ERP.Bll.UserRole;
using ERP.CoreDB;
using ERP.Filters;
using ERP.Helper.Helper.TemplateView;
using ERP.WorkerService;
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
// Aplicativo
builder.Services.AddScoped<ITemplateViewHelper, TemplateViewHelper>();
// Bll
builder.Services.AddScoped<IAuthenticationBll, AuthenticationBll>();
builder.Services.AddScoped<IProfileBll, ProfileBll>();
builder.Services.AddScoped<IUserBll, UserBll>();
<<<<<<< Updated upstream
builder.Services.AddScoped<IInvoiceBll, InvoiceBll>();
=======
builder.Services.AddScoped<IUserRoleBll, UserRoleBll>();
builder.Services.AddScoped<IPaisBll, PaisBll>();
builder.Services.AddScoped<IClientBll, ClientBll>();
builder.Services.AddScoped<IRoleBll, RoleBll>();
builder.Services.AddScoped<IPointIssueBll, PointIssueBll>();
builder.Services.AddScoped<IPointSaleBll, PointSaleBll>();


>>>>>>> Stashed changes
// :: Interfaces - aplicativo (fin)


builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

// Filter
builder.Services.AddScoped<SessionUserFilter>();

builder.Services.AddHostedService<ProcessWorkerService>();

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
