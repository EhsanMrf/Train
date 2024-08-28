using Common.OperationCrud;
using Common.TransientService;
using Domain.Model.Model.Book;
using Infrastructure.Database;
using Infrastructure.MediatR;
using Infrastructure.Repository.Book;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
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

    public static void SingletonCrudManager(this IServiceCollection services)
    {
        services.Add(new ServiceDescriptor(typeof(ICrudManager<Book, Guid, DatabaseContext>),
            typeof(CrudManager<Book, Guid, DatabaseContext>), ServiceLifetime.Scoped));

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

    public static void BeforeRequestInPipeLine(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ManageExceptionBehavior<,,>));
    }
}