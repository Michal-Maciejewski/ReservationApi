using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using ReservationApi.Data;
using System.Reflection;
using System.Text;

namespace ReservationApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var startup = new Startup(builder.Configuration, builder.Environment);

            // Add services to the container.
            startup.ConfigureDatabase(builder.Services);

            //Configure Mapping
            startup.ConfigureMapster(builder.Services);

            // Add Controllers
            startup.ConfigureControllers(builder.Services);

            //Configure Authentication for JWT
            startup.ConfigureAuthentication(builder.Services);

            var app = builder.Build();

            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseStaticFiles();
                startup.SeedDatabase(app.Services);
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.InjectStylesheet("/css/swagger-ui.css");
                });
            }
            
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}