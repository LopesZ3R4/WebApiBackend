// File Path: c:\WebApiBackend\Program.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Services; // Add this line if AuthenticationService is in the Services namespace
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient; // Add this line if UserRepository is in the Data namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
try
{
    using var connection = new SqlConnection(connectionString);
    connection.Open(); // Try to open the connection
}
catch
{
    // If the connection fails, switch to the fallback connection
    connectionString = builder.Configuration.GetConnectionString("FallbackConnection");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// Register AuthenticationService and UserRepository
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<UserRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();