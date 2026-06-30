using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using turismo_oro.Application.Interfaces;
using turismo_oro.Application.Services;
using turismo_oro.Infrastructure.Data;
using turismo_oro.Infrastructure.Repositories;
using turismo_oro.Infrastructure.Services;

namespace turismo_oro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SqlDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITurismoLugarRepository, TurismoLugarRepository>();
        services.AddScoped<IArchivoRepository, ArchivoRepository>();
        services.AddScoped<IComentarioRepository, ComentarioRepository>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<TurismoLugarService>();

        return services;
    }
}
