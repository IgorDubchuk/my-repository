using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Infrastructure.Persistence;
using FluentValidation;
using API.Contracts.ConsumedEvents;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog(configureLogger: (context, configuration) =>
            {
                configuration.Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(
                            new Uri(context.Configuration["ElasticConfiguration:Uri"]!))
                        {
                            IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                            AutoRegisterTemplate = true,
                            NumberOfShards = 1,
                            NumberOfReplicas = 1,
                        })
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName!)
                        .ReadFrom.Configuration(context.Configuration);

            });

            // Add services to the container.
            builder.Services.AddAndConfigureApi();
            builder.Services.AddAndConfigureSwagger();
            builder.Services.AddDbContext<F1DbContext>(a => a.UseSqlServer(builder.Configuration.GetConnectionString("LocalF1SqlServerConnection")));
            builder.Services.AddServices();
            builder.Services.AddRepositories();
            //builder.Services.AddValidatorsFromAssemblyContaining<ConsumedEventDtoValidator>(ServiceLifetime.Transient);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAndConfigureSwagger();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}