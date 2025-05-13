using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class Extensions
{
    public static IServiceCollection AddDataBaseService(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddDbContext<AppContext>(x => { x.UseNpgsql(
                    "Server=localhost;Port=5432;Database=Aton;User Id=Aton;Password=1234567890", 
                    b => b.MigrationsAssembly("AtonWebAPI"));
            
            });
            
    }
}