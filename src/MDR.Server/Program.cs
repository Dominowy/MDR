using MDR.Application;
using MDR.Application.Devices.Commands;
using MDR.Application.Devices.Queries;
using MDR.Infrastructure;
using MDR.Server;
using MDR.Server.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var key = Encoding.UTF8.GetBytes("super_super_secret_key_that_is_at_least_32_chars_long!!");

// 🔐 JWT Auth
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("OperatorOrAdmin", policy => policy.RequireRole("Operator", "Admin"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication(configuration);

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/generate", (string username, string role) =>
{
    var securityKey = new SymmetricSecurityKey(key);
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, role)
    };

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
    return Results.Ok(new { token = jwt });
}).AllowAnonymous();

app.MapPost("/get", async (IMediator mediator, HttpContext httpContext, [FromBody] GetDeviceRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("GetDevice")
    .WithOpenApi();

app.MapPost("/get-all", async (IMediator mediator, HttpContext httpContext, [FromBody] GetAllDevicesRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("GetAllDevices")
    .WithOpenApi();

app.MapPost("/add/form", async (IMediator mediator, HttpContext httpContext, [FromBody] GetAddDeviceFormRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("GetAddFormDevice")
    .WithOpenApi()
    .RequireAuthorization("AdminOnly");

app.MapPost("/add", async (IMediator mediator, HttpContext httpContext, [FromBody] AddDeviceRequest request) 
    => await httpContext.Send(request, mediator))
    .WithName("AddDevice")
    .WithOpenApi()
    .RequireAuthorization("AdminOnly");

app.MapPost("/add/validate", async (IMediator mediator, HttpContext httpContext, [FromBody] AddDeviceRequest request)
    => await httpContext.Validate(request, mediator))
    .WithName("AddDeviceValidate")
    .WithOpenApi()
    .RequireAuthorization("AdminOnly");

app.MapPost("/update/form", async (IMediator mediator, HttpContext httpContext, [FromBody] GetUpdateDeviceFormRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("GetUpdateFormDevice")
    .WithOpenApi()
    .RequireAuthorization("AdminOnly");

app.MapPost("/update", async (IMediator mediator, HttpContext httpContext, [FromBody] UpdateDeviceRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("UpdateDevice")
    .WithOpenApi()
    .RequireAuthorization("AdminOnly");

app.MapPost("/update/validate", async (IMediator mediator, HttpContext httpContext, [FromBody] UpdateDeviceRequest request)
    => await httpContext.Validate(request, mediator))
    .WithName("UpdateDeviceValidate")
    .WithOpenApi()
    .RequireAuthorization("AdminOnly");

app.MapPost("/delete", async (IMediator mediator, HttpContext httpContext, [FromBody] DeleteDeviceRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("DeleteDevice")
    .WithOpenApi()
    .RequireAuthorization("AdminOnly");

app.MapPost("/get-data-from-device", async (IMediator mediator, HttpContext httpContext, [FromBody] GetDataFromDeviceRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("GetDataFromDevice")
    .WithOpenApi()
    .RequireAuthorization("OperatorOrAdmin");

app.MapPost("/send-data-from-device", async (IMediator mediator, HttpContext httpContext, [FromBody] SendDataFromDeviceRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("SendDataFromDevice")
    .WithOpenApi()
    .RequireAuthorization("OperatorOrAdmin");

app.MapPost("/get-all-data", async (IMediator mediator, HttpContext httpContext, [FromBody] GetAllDataRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("GetAllData")
    .WithOpenApi()
    .RequireAuthorization("OperatorOrAdmin");

app.Run();
