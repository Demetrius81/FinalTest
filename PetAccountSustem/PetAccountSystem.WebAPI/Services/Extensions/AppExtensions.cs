using Microsoft.EntityFrameworkCore;
using PetAccountSystem.WebAPI.Context;
using System.Runtime.CompilerServices;

namespace PetAccountSystem.WebAPI.Services.Extensions;

internal static class AppExtensions
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static WebApplicationBuilder RegisterDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Sqlite");


        builder.Services.AddDbContext<PetDBContext>(opt => opt.UseSqlite(connectionString));


        return builder;
    }
}
