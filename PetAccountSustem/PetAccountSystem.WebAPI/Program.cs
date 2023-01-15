using PetAccountSystem.WebAPI.Data;
using PetAccountSystem.WebAPI.Services.Extensions;

namespace PetAccountSystem.WebAPI;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var app = WebApplication
            .CreateBuilder(args)
            .RegisterServices()
            .RegisterDbContext()
            .Build();

        using (var scope = app.Services.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<AppDbInitializer>();
            await initializer.InitializeAsync(removeBefore: true);
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
