using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// 1. Add Services to the Container
// ---------------------------------------------------------

builder.Services.AddControllers();

// CORS Configuration with named policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

// JWT Authentication
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
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
              RoleClaimType = ClaimTypes.Role
        };
    });

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Swagger Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Easypay API",
        Version = "v1.0"
    });

    // Add JWT authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<IAuditLogRepo<AuditLog>, AuditLogRepo>();
builder.Services.AddScoped<IBenefitRepo<Benefit>, BenefitRepo>();
builder.Services.AddScoped<IEmployeeRepo<Employee>, EmployeeRepo>();
builder.Services.AddScoped<ILeaveRequestRepo<LeaveRequest>, LeaveRequestRepo>();
builder.Services.AddScoped<INotificationRepo<Notification>, NotificationRepo>();
builder.Services.AddScoped<IPayrollConfigRepo<PayrollConfig>, PayrollConfigRepo>();
builder.Services.AddScoped<IPayrollRepo<Payroll>, PayrollRepo>();
builder.Services.AddScoped<IReportRepo, ReportRepo>();
builder.Services.AddScoped<ITimesheetRepo<Timesheet>, TimesheetRepo>();
builder.Services.AddScoped<IUserRepo<User>, UserRepo>();

var app = builder.Build();

// ---------------------------------------------------------
// 2. Configure the HTTP Request Pipeline
// ---------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// CORS middleware - must come before other middleware
app.UseCors("AngularApp");

// Custom middleware to handle raw tokens
app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
    if (!string.IsNullOrEmpty(authHeader) && !authHeader.StartsWith("Bearer "))
    {
        context.Request.Headers["Authorization"] = $"Bearer {authHeader}";
    }
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

// ✅ Enable response to OPTIONS requests for all endpoints
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type,Authorization");
        context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
    }
    else
    {
        await next();
    }
});

app.MapControllers();

app.Run();
