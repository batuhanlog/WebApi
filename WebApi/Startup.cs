using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperation;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApi.Middlewares;
using WebApi.Services;
using WebApi.DBOperations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Token:Issuer"],
                ValidAudience = Configuration["Token:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                ClockSkew = TimeSpan.Zero


            };
        });
        services.AddDbContext<BookStoreDbContext>(options =>
            options.UseInMemoryDatabase("BookStoreDB"));
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
        });
        services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddSingleton<ILoggerService, DBLogger>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Initialize data.
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            DataGenerator.Initialize(serviceProvider);
        }

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(
                c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

        }
        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCustomExceptionMiddleware();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
