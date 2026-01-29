using AcademyIO.WebAPI.Core.DatabaseFlavor;
using AcademyIO.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using static AcademyIO.WebAPI.Core.DatabaseFlavor.ProviderConfiguration;

namespace AcademyIO.WebAPI.Core.Configuration;

public static class ApiCoreConfig
{
    public static void AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog(new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger());
    }
    
    public static IServiceCollection AddApiCoreConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenApiDocument();

        services.AddDefaultHealthCheck(configuration);

        services.AddControllers();

        // CORS Configuration
        // Em desenvolvimento: permite qualquer origem
        // Em producao: restringe a origens especificas via configuracao
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
            ?? new[] { "http://localhost:4200", "http://localhost:3000" };

        services.AddCors(options =>
        {
            // Politica restritiva para producao
            options.AddPolicy("Production",
                builder =>
                    builder
                        .WithOrigins(allowedOrigins)
                        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                        .WithHeaders("Authorization", "Content-Type", "Accept", "X-Requested-With")
                        .AllowCredentials());

            // Politica permissiva apenas para desenvolvimento
            options.AddPolicy("Development",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            // Politica padrao (alias para compatibilidade)
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });

        return services;
    }

    public static IServiceCollection WithDbContext<TContext>(this IServiceCollection services,
        IConfiguration configuration) where TContext : DbContext
    {
        services.ConfigureProviderForContext<TContext>(DetectDatabase(configuration));

        return services;
    }

    public static void UseApiCoreConfiguration(this WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseOpenApi();
            app.UseSwaggerUi();
            app.UseReDoc(options => { options.Path = "/redoc"; });
        }

        // Under certain scenarios, e.g. minikube / linux environment / behind load balancer
        // https redirection could lead devs to overcomplicate configuration for testing purposes
        // In production is a good practice to keep it true
        if (app.Configuration["USE_HTTPS_REDIRECTION"] == "true")
            app.UseHttpsRedirection();

        app.UseRouting();

        // Usar politica de CORS apropriada para o ambiente
        var corsPolicy = env.IsDevelopment() ? "Development" : "Production";
        app.UseCors(corsPolicy);

        app.UseAuthConfiguration();

        app.MapControllers();

        app.UseDefaultHealthcheck();
    }
}