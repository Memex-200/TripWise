using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TripWise.Application.Interfaces.Repositories;
using TripWise.Application.Interfaces.Services;
using TripWise.Domain.Entities;
using TripWise.Infrastructure.Repositories;
using TripWise.Infrastructure.Services;
using TripWise.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ✅ Enable Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

try
{
    Console.WriteLine("🚀 Application Starting...");

    // ✅ Load Configuration
    var configuration = builder.Configuration;

    // 🔹 Ensure JWT Configuration Exists
    var jwtSettings = configuration.GetSection("Jwt");
    if (string.IsNullOrEmpty(jwtSettings["Key"]))
    {
        throw new InvalidOperationException("🚨 JWT Key is missing in appsettings.json!");
    }

    // ✅ Configure Database Context (SQL Server)
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            sqlOptions => sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName) // Ensure migrations are linked
        )
    );

    // ✅ Configure Identity (Using Customer as the User)
    builder.Services.AddIdentity<Customer, IdentityRole<int>>(options =>
    {

        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

    // ✅ Configure JWT Authentication
    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = !string.IsNullOrEmpty(jwtSettings["Issuer"]),
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = !string.IsNullOrEmpty(jwtSettings["Audience"]),
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

    // ✅ Configure Authorization
    builder.Services.AddAuthorization();

    // ✅ Register Repositories (Dependency Injection)
    builder.Services.AddScoped<ITransportCompanyRepository, TransportCompanyRepository>();
    builder.Services.AddScoped<IHotelRepository, HotelRepository>();
    builder.Services.AddScoped<IOfferRepository, OfferRepository>();

    // ✅ Register Services (Dependency Injection)
    builder.Services.AddScoped<IHotelService, TripWise.Infrastructure.Services.HotelService>();
    builder.Services.AddScoped<IOfferService, OfferService>();
    builder.Services.AddScoped<ITransportCompanyService, TransportCompanyService>();

    // ✅ Configure CORS Policy (Allow Specific Domains for Production)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins",
            corsPolicy => corsPolicy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
    });

    // ✅ Add API Controllers
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // ✅ Ensure Database is Migrated
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<ApplicationDbContext>();

        try
        {
            Console.WriteLine("📌 Applying Database Migrations...");
            dbContext.Database.Migrate();
            Console.WriteLine("✅ Database Migration Completed.");
        }
        catch (Exception dbEx)
        {
            Console.WriteLine($"🚨 Error Migrating Database: {dbEx.Message}");
            throw;
        }
    }

    // ✅ Global Exception Handling Middleware
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            Console.WriteLine("🚨 An unexpected error occurred.");
            await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
        });
    });

    // ✅ Enable Swagger (For Development Mode)
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // ✅ Enable CORS
    app.UseCors("AllowSpecificOrigins");

    // ✅ Enable Authentication & Authorization
    app.UseAuthentication();
    app.UseAuthorization();

    // ✅ Map API Controllers
    app.MapControllers();

    // ✅ Start the Application
    Console.WriteLine("✅ Application Started Successfully.");
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"🚨 Fatal Error: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    throw;
}
