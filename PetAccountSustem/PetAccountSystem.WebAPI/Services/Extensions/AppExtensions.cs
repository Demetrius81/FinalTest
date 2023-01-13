using Microsoft.EntityFrameworkCore;
using PetAccountSystem.DAL.Context;
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
        var connectionType = builder.Configuration["Database"];
        var connectionString = builder.Configuration.GetConnectionString(connectionType);

        switch (connectionType)
        {
            case "Sqlite":
                builder.Services.AddDbContext<PetDBContext>(opt => opt.UseSqlite(connectionString, o => o.MigrationsAssembly("PetAccountSystem.DAL.Sqlite")));
                break;
            //case "SqlServer":
            //    builder.Services.AddDbContext<PetDBContext>(opt => opt.UseSqlServer(connectionString, o => o.MigrationsAssembly("PetAccountSystem.DAL.SqlServer")));
            //    break;
            //case "NpgSql":
            //    builder.Services.AddDbContext<PetDBContext>(opt => opt.UseNpgSql(connectionString, o => o.MigrationsAssembly("PetAccountSystem.DAL.NpgSql")));
            //    break;
            default:
                break;
        };

        return builder;
    }
}
