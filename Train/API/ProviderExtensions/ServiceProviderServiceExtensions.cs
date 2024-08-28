using Common.TransientService;
using Infrastructure.Database;
using Infrastructure.Repository.Book;
using Microsoft.EntityFrameworkCore;

namespace API.ProviderExtensions;

public static class ServiceProviderServiceExtensions
{    public static void InjectScope(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssembliesOf(typeof(BookCommandRepository))
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public static void DatabaseContext(this IServiceCollection services, string connection)
    {
        services.AddDbContextFactory<DatabaseContext>(x =>
            x.UseSqlServer(connection));
    }

    public static void UpgradeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        db.Database.Migrate();
    }
}