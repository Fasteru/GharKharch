using GharKharchaAPI.Application.Features;
using GharKharchaAPI.Authentication;
using GharKharchaAPI.Data.Context;
using GharKharchaAPI.Helper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:7044");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<BudgetService>();
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();