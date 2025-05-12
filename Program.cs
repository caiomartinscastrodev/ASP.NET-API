using API.Context;
using API.IRepository;
using API.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositorys
builder.Services.AddScoped<IPaiRepository , PaiRepository>();
builder.Services.AddScoped<IFilhoRepository , FilhoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Service
builder.Services.AddScoped<TokenService>();

string connectionDb = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
string secret = builder.Configuration["Jwt:SecretKey"] ?? "";
string issuer = builder.Configuration["Jwt:Issuer"] ?? "";
string audience = builder.Configuration["Jwt:Audience"] ?? "";

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionDb , ServerVersion.AutoDetect(connectionDb)));

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer" , options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
