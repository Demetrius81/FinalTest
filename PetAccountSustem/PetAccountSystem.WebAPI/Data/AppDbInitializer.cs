using Microsoft.EntityFrameworkCore;
using PetAccountSystem.Models.Models;
using PetAccountSystem.WebAPI.Data.Context;

namespace PetAccountSystem.WebAPI.Data;

public class AppDbInitializer
{
    private readonly PetDBContext _dbContext;
    private readonly ILogger<AppDbInitializer> _logger;

    public AppDbInitializer(PetDBContext dbContext, ILogger<AppDbInitializer> logger)
    {
        this._dbContext = dbContext;
        this._logger = logger;
    }

    public async Task<bool> RemoveAsync(CancellationToken cancel = default)
    {
        if (!await this._dbContext.Database.EnsureDeletedAsync(cancel).ConfigureAwait(false))
        {
            this._logger.LogInformation("> No database");
            return false;
        }

        this._logger.LogInformation("> Database deleted");
        return true;
    }

    public async Task InitializeAsync(bool removeBefore = false, CancellationToken cancel = default)
    {
        if (removeBefore)
        {
            await RemoveAsync(cancel).ConfigureAwait(false);
        }

        await this._dbContext.Database.MigrateAsync(cancel).ConfigureAwait(false);
        this._logger.LogInformation("> The database has been created and its status is up to date");
        await AddTestDataAsync(cancel);
    }

    public async Task AddTestDataAsync(CancellationToken cancel = default)
    {
        if (!await this._dbContext.Pets.AnyAsync(cancel).ConfigureAwait(false))
        {
            var rnd = new Random();
            var pets = Enumerable.Range(1, 10).Select(p => new Pet
            {
                KindOfAnimal = $"Kind of animal {p}",
                IsPackAnimal = p % 2 == 0,
                Count = rnd.Next(1, 9),
            });
            await this._dbContext.Pets.AddRangeAsync(pets, cancel);
            await this._dbContext.SaveChangesAsync(cancel);
            this._logger.LogInformation("> Test data added to database");
        }
    }
}
