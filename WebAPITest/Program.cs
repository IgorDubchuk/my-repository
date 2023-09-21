using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
using WebAPITest.Infrastructure.Persistence;

namespace WebAPITest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAndConfigureApi();
            builder.Services.AddAndConfigureSwagger();
            builder.Services.AddDbContext<F1DbContext>(a => a.UseSqlServer(builder.Configuration.GetConnectionString("LocalF1SqlServerConnection")));
            builder.Services.AddServices();
            builder.Services.AddRepositories();

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