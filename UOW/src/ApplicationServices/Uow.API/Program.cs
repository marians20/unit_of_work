using Uow.API.Extensions;
using Uow.API.Middleware;
using Uow.Data;
using Uow.Domain;

namespace Uow.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddHttpContextAccessor()
            .RegisterData(builder.Configuration)
            .RegisterDomain();

        builder.Services.AddControllers();
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

        app.UseAuthorization();

        app.UseMiddleware<SantitizationMiddleware>();

        app.MapControllers();

        app.Run();
    }
}