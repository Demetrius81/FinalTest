using Microsoft.EntityFrameworkCore;
using PetAccountSystem.Interfaces.Repositories;
using PetAccountSystem.WebAPI.Data;
using PetAccountSystem.WebAPI.Data.Context;
using PetAccountSystem.WebAPI.Services.Repository;
using System.Runtime.CompilerServices;

namespace PetAccountSystem.WebAPI.Services.Extensions;

internal static class AppExtensions
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped(typeof(IRepositoryAsync<>), typeof(EntityRepository<>));

        return builder;
    }

    public static WebApplicationBuilder RegisterDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Sqlite");

        builder.Services.AddDbContext<PetDBContext>(opt => opt.UseSqlite(connectionString));
        builder.Services.AddTransient<AppDbInitializer>();

        return builder;
    }
}
