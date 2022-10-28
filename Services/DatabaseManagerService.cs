using HiveManager.Data;
using Microsoft.EntityFrameworkCore;

namespace HiveManager.Services;

public static class DatabaseManagerService
{
    public static void MigrationInitialisation(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceDb = serviceScope.ServiceProvider.GetService<HiveContext>();
            serviceDb.Database.Migrate();
        }
    }
}

