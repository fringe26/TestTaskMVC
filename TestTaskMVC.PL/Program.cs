using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTaskMVC.DAL.DataBase;
using Microsoft.Extensions.Configuration;
using TestTaskMVC.DomainModels.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System;
using System.Configuration;
using TestTaskMVC.BL.Services;
using TestTaskMVC.BL.Interfaces;
using TestTaskMVC.DAL.IRepository;
using TestTaskMVC.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("PgSqlDb"))

).AddIdentityCore<AppUser>();

builder.Services.AddMvcCore().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
                = ReferenceLoopHandling.Ignore);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowCors",
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                     
                      });
});


builder.Services.AddIdentity<AppUser,IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IClientService,ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();


builder.Services.AddSingleton(builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("_myAllowCors");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
