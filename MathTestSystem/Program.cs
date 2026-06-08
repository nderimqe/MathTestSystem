using MathTestSystem.Application.Interfaces;
using MathTestSystem.Application.Services;
using MathTestSystem.Infrastructure.Data;
using MathTestSystem.Infrastructure.Repositories;
using MathTestSystem.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IXMLService, XMLService>();
builder.Services.AddScoped<IMathProcessor, MathProcessor>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IExamResultRepository, ExamResultRepository>();

builder.Services.AddDbContext<MathTestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MathTestDb")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MathTestDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
   
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teacher}/{action=Index}/{id?}");

app.Run();
