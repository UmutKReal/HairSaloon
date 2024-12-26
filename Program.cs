using BarberSaloon.Data;
using BarberSaloon.Models;
using BarberSaloon.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();

//string connectionString = builder.Configuration.GetConnectionString("ConnectionStrings");

builder.Services.AddDbContext<BarberSaloonDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services in the DI container
builder.Services.AddHttpClient();  // Registers HttpClient for dependency injection
builder.Services.AddControllers();
builder.Services.AddHttpClient<OpenAiImageService>();

builder.Services.AddControllersWithViews();  // Add MVC controllers and views   Add services to the container.

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();