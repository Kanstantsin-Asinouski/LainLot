using System.Text;
using Authentication.Services;
using Authentication.Configuration;
using Newtonsoft.Json;
using DatabaseProvider.Models;
using DatabaseRepository.Classes;
using DatabaseRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LainLot API. Authentication service",
        Version = "v1",
        Description = "API for LainLot. Authorization via email and password. Swagger automatically receives a token.."
    });

    var jwtScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter your email and password via the 'Log in' button. The token will be received automatically.",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    };

    c.AddSecurityDefinition(jwtScheme.Reference.Id, jwtScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtScheme, Array.Empty<string>() }
    });

    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        return apiDesc.ActionDescriptor?.DisplayName?.Contains("Authentication") == true;
    });
});

var HostOrigins = "HostOrigins";
var corsAdresses = new string[]
{
    "http://localhost:3000", // adminPanel
    "http://localhost:3001", // atelier
    "http://localhost:3002", // shop
    "http://localhost:8040", // RestAPI IIS
    "https://localhost:5040", // RestAPI https
    "https://lainlot.com"    // PROD
};

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: HostOrigins,
        policy =>
        {
            policy.WithOrigins(corsAdresses)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LainLotContext>(options =>
    options.UseNpgsql(connectionString));

var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddScoped<IRepository<About>, Repository<About>>();
builder.Services.AddScoped<IRepository<AccessLevel>, Repository<AccessLevel>>();
builder.Services.AddScoped<IRepository<BaseBelt>, Repository<BaseBelt>>();
builder.Services.AddScoped<IRepository<BaseNeckline>, Repository<BaseNeckline>>();
builder.Services.AddScoped<IRepository<BasePant>, Repository<BasePant>>();
builder.Services.AddScoped<IRepository<BasePantsCuff>, Repository<BasePantsCuff>>();
builder.Services.AddScoped<IRepository<BaseSleeve>, Repository<BaseSleeve>>();
builder.Services.AddScoped<IRepository<BaseSleeveCuff>, Repository<BaseSleeveCuff>>();
builder.Services.AddScoped<IRepository<BaseSportSuit>, Repository<BaseSportSuit>>();
builder.Services.AddScoped<IRepository<BaseSweater>, Repository<BaseSweater>>();
builder.Services.AddScoped<IRepository<Cart>, Repository<Cart>>();
builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
builder.Services.AddScoped<IRepository<CategoryHierarchy>, Repository<CategoryHierarchy>>();
builder.Services.AddScoped<IRepository<Color>, Repository<Color>>();
builder.Services.AddScoped<IRepository<Contact>, Repository<Contact>>();
builder.Services.AddScoped<IRepository<Country>, Repository<Country>>();
builder.Services.AddScoped<IRepository<Currency>, Repository<Currency>>();
builder.Services.AddScoped<IRepository<CustomBelt>, Repository<CustomBelt>>();
builder.Services.AddScoped<IRepository<CustomNeckline>, Repository<CustomNeckline>>();
builder.Services.AddScoped<IRepository<CustomPant>, Repository<CustomPant>>();
builder.Services.AddScoped<IRepository<CustomPantsCuff>, Repository<CustomPantsCuff>>();
builder.Services.AddScoped<IRepository<CustomSleeve>, Repository<CustomSleeve>>();
builder.Services.AddScoped<IRepository<CustomSleeveCuff>, Repository<CustomSleeveCuff>>();
builder.Services.AddScoped<IRepository<CustomSportSuit>, Repository<CustomSportSuit>>();
builder.Services.AddScoped<IRepository<CustomSweater>, Repository<CustomSweater>>();
builder.Services.AddScoped<IRepository<CustomizableProduct>, Repository<CustomizableProduct>>();
builder.Services.AddScoped<IRepository<FabricType>, Repository<FabricType>>();
builder.Services.AddScoped<IRepository<Language>, Repository<Language>>();
builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();
builder.Services.AddScoped<IRepository<OrderHistory>, Repository<OrderHistory>>();
builder.Services.AddScoped<IRepository<OrderStatus>, Repository<OrderStatus>>();
builder.Services.AddScoped<IRepository<Payment>, Repository<Payment>>();
builder.Services.AddScoped<IRepository<PaymentMethod>, Repository<PaymentMethod>>();
builder.Services.AddScoped<IRepository<PaymentStatus>, Repository<PaymentStatus>>();
builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
builder.Services.AddScoped<IRepository<ProductImage>, Repository<ProductImage>>();
builder.Services.AddScoped<IRepository<ProductOrder>, Repository<ProductOrder>>();
builder.Services.AddScoped<IRepository<ProductTranslation>, Repository<ProductTranslation>>();
builder.Services.AddScoped<IRepository<Review>, Repository<Review>>();
builder.Services.AddScoped<IRepository<ShippingAddress>, Repository<ShippingAddress>>();
builder.Services.AddScoped<IRepository<SizeOption>, Repository<SizeOption>>();
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<UserOrderHistory>, Repository<UserOrderHistory>>();
builder.Services.AddScoped<IRepository<UserProfile>, Repository<UserProfile>>();
builder.Services.AddScoped<IRepository<UserRole>, Repository<UserRole>>();

// Add auth service
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<EmailService>();

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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LainLot API V1");

        c.InjectJavascript("/swagger-login.js");
    });
}

app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("Hello from ASP.NET Core with Serilog!");
    return "Hello World!";
});

app.UseCors(HostOrigins);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();