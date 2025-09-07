using MDR.Application;
using MDR.Application.Devices.Commands;
using MDR.Application.Devices.Queries;
using MDR.Infrastructure;
using MDR.Server;
using MDR.Server.Middleware;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

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

app.MapPost("/add/form", async (IMediator mediator, HttpContext httpContext, [FromBody] GetAddDeviceFormRequest request)
    => await httpContext.Send(request, mediator))
    .WithName("GetAddFormDevice")
    .WithOpenApi();

app.MapPost("/add", async (IMediator mediator, HttpContext httpContext, [FromBody] AddDeviceRequest request) 
    => await httpContext.Send(request, mediator))
    .WithName("AddDevice")
    .WithOpenApi();

app.Run();
