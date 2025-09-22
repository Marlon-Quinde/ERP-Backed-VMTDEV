using ERP.Bll.Invoice;
using ERP.Bll.Security.Authentication;
using ERP.Bll.Security.Profile;
using ERP.Bll.User;
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
builder.Services.AddScoped<IInvoiceBll, InvoiceBll>();
// :: Interfaces - aplicativo (fin)


builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

// Filter
builder.Services.AddScoped<SessionUserFilter>();

builder.Services.AddHostedService<ProcessWorkerService>();


builder.Services.AddCors(o => o.AddPolicy("AllowDev", p =>
    //p.WithOrigins("http://localhost:5173", "http://127.0.0.1:5500", "http://localhost")
    p.AllowAnyOrigin()
     .AllowAnyHeader()
     .AllowAnyMethod()
));



var app = builder.Build();

app.UseCors("AllowDev");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
