using BarberSaloon.Data;
using BarberSaloon.Models;
using BarberSaloon.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();

builder.Services.AddDbContext<BarberSaloonDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services in the DI container
builder.Services.AddHttpClient();  // Registers HttpClient for dependency injection
builder.Services.AddControllers();
builder.Services.AddHttpClient<OpenAiImageService>();

builder.Services.AddControllersWithViews();  // Add MVC controllers and views

// Add Authentication with Cookie Scheme
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Specify the login path
        options.LogoutPath = "/Account/Logout"; // Specify the logout path
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // Add Authentication Middleware
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map API controllers
app.MapControllers();

app.Run();