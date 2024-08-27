using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;
using Persistence.Redis;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        const string dbConnection = "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
        
        services.AddDbContext<TableContext>(opt => opt.UseSqlServer(dbConnection));
        services.AddScoped<ITableSpecificationRepository, TableSpecificationRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<ITodoDetailRepository, TodoDetailRepository>();

        return services;
    }

    public static IServiceCollection AddRedisServices(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfig = configuration.GetConnectionString("Redis") ?? "localhost:6379";
        services.AddSingleton(new RedisServer(redisConfig));
        services.AddSingleton<ICacheService, RedisCacheService>();
        return services;
    }

}