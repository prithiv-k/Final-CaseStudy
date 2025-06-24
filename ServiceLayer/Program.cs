using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

//  JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//  API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); //  /api/v1.0/controller
});

// 🧪 Swagger with manual token entry
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EasyPay API",
        Version = "v1.0"
    });

    // Accept plain JWT without auto Bearer prefix
    options.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
    {
        Description = "Enter Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "JWT"
                }
            },
            Array.Empty<string>()
        }
    });
});

//  Register your repositories
builder.Services.AddScoped<IAuditLogRepo<AuditLog>, AuditLogRepo>();
builder.Services.AddScoped<IBenefitRepo<Benefit>, BenefitRepo>();
builder.Services.AddScoped<IEmployeeRepo<Employee>, EmployeeRepo>();
builder.Services.AddScoped<ILeaveRequestRepo<LeaveRequest>, LeaveRequestRepo>();
builder.Services.AddScoped<INotificationRepo<Notification>, NotificationRepo>();
builder.Services.AddScoped<IPayrollConfigRepo<PayrollConfig>, PayrollConfigRepo>();
builder.Services.AddScoped<IPayrollRepo<Payroll>, PayrollRepo>();
builder.Services.AddScoped<ITimesheetRepo<Timesheet>, TimesheetRepo>();
builder.Services.AddScoped<IUserRepo<User>, UserRepo>();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyPay API v1.0");
    });
}

app.UseHttpsRedirection();

//  Middleware: Prepend 'Bearer' to raw tokens
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

app.MapControllers();

app.Run();
