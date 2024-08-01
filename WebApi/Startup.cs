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

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<BookStoreDbContext>(options =>
            options.UseInMemoryDatabase("BookStoreDB"));
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
        });
        services.AddDbContext<BookStoreDbContext>(options =>options.UseInMemoryDatabase(databaseName:"DevConnection"));
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
        }

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
