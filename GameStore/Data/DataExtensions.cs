using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace GameStore.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app){

        //creating a scope variable for allocating the resources for certain period 
        //scope injects
        using var scope=app.Services.CreateScope();

        // Dependency injection
        //it retrieves the the instances
        var DbContext=scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        //it automatically apply changes
        await DbContext.Database.MigrateAsync();
    }


}
