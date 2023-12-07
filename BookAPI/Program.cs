using BookAPI.Data;
using Microsoft.AspNetCore.Identity;
using BookAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using BookAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BookAPI.Data.Entities;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// swagger documentation setup
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Fast api",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
builder.Services.AddScoped<IAuthService, AuthService>();



// authentication scheme setup
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value);
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };

});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{

}).AddEntityFrameworkStores<BookDbContext>();


builder.Services.AddAuthorization(options =>
{
    
});


builder.Services.AddDbContext<BookDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionString:default").Value);
});


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
