using PetAccountSystem.WebAPI.Services.Extensions;

namespace PetAccountSystem.WebAPI;

internal class Program
{
    protected Program() { }

    private static void Main(string[] args)
    {
        var app = WebApplication
            .CreateBuilder(args)
            .RegisterServices()
            .Build();

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