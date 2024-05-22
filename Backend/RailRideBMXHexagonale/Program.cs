using System.Text;
using Stripe;
using Application;
using Application.IServices;
using Application.Services;
using CloudinaryDotNet;
using Core.Domain.Entity;
using Infrastructure.DbContext;
using Infrastructure.Adapters;
using Infrastructure.Ports;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RailRideBMXHexagonale.Middleware;
using Account = CloudinaryDotNet.Account;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;

    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

    if (env.EnvironmentName == "Docker")
    {
        config.AddJsonFile("appsettings.docker.json", optional: true, reloadOnChange: true);
    }
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseConnection")));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>(); 
builder.Services.AddScoped<IProductService, ProductsService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ILikeService, LikeService>();

builder.Services.AddScoped<IConfigurationBMXRepository, ConfigurationBMXRepository>();
builder.Services.AddScoped<IConfigurationBMXService, ConfigurationBmxService>();

builder.Services.AddScoped<IAdressRepository, AdressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();



builder.Services.AddScoped<IStripeService, StripeService>();

builder.Services.AddHostedService<TokenCleanupService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builders =>
    {
        builders.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Type");
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthentication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(7);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
StripeConfiguration.ApiKey = "sk_test_51OVAt3ClaClP8ajK7CxwZkmknJUIJLRm4tjHytQKSNDkkYqxkRZU4qiHLDYONUIfIeSnOEmftzE9eAIxBNWWzUmB005vBtH7TE";

Account account = new Account(
    "dnmiqn9pk",
    "726578711948311",
    "jTPn4MV_jx_PQD4oUYtSAVkd2A0");
Cloudinary cloudinary = new Cloudinary(account);
cloudinary.Api.Secure = true;
builder.Services.AddSingleton(cloudinary);


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Mettre Swagger UI à la racine de l'application
});
app.UseSession();
app.UseCors();
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<SessionCookieMiddleware>();
app.MapControllers();
app.Run();
