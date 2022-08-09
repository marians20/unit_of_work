// <copyright file="Program.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

using Uow.API.Extensions;
using Uow.API.Middleware;
using Uow.API.Auth.Extensions;
using Uow.API.Filters;
using Uow.Application.Bootstrap;
using Uow.Data;

namespace Uow.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddJwtTokenServices(builder.Configuration);

        // Add services to the container.
        builder.Services
            .AddHttpContextAccessor()
            .RegisterData(builder.Configuration)
            .RegisterDomain();

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionFilter>();
            options.Filters.Add<ModelValidationErrorHandlerFilter>();
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<SantitizationMiddleware>();

        app.MapControllers();

        app.Run();
    }
}