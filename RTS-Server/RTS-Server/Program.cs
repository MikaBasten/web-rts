using RTS_Server.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Add this

using Core.Models;
using Core.IRepository;
using Core.IServices;
using Core.Services;
using DAL.DB;
using DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
var siteBUrl = builder.Configuration["SiteBUrl"];

// Register your connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("WebRTS")); // Use In-Memory database for development

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); // Register PasswordHasher

// Configure CORS to allow a specific origin
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500") // Allow only this origin
              .AllowAnyMethod()    // Allow all HTTP methods (GET, POST, etc.)
              .AllowAnyHeader()    // Allow all headers
              .AllowCredentials(); // Allow credentials (cookies, authorization headers, etc.)
    });
});

// Add SignalR service
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Use CORS middleware
app.UseCors();

app.MapControllers();

// Map the SignalR hub
app.MapHub<GameHub>("/gameHub");

app.Run();