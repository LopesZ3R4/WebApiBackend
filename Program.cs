// File Path: c:\WebApiBackend\Program.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "backendapplication",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@Senhamuitoforte1234"))
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
try
{
    using var connection = new SqlConnection(connectionString);
    connection.Open();
}
catch
{
    connectionString = builder.Configuration.GetConnectionString("FallbackConnection");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
    
// Register AuthenticationService and UserRepository
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AlertRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();