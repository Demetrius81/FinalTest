using Microsoft.EntityFrameworkCore;
using PetAccountSystem.Interfaces.Entities;
using PetAccountSystem.Interfaces.Repositories;
using PetAccountSystem.WebAPI.Data.Context;

namespace PetAccountSystem.WebAPI.Services.Repository;

public class EntityRepository<T> : IRepositoryAsync<T> where T : class, IEntity
{
    private readonly PetDBContext _dBContext;
    private readonly ILogger<EntityRepository<T>> _logger;

    public EntityRepository(PetDBContext dBContext, ILogger<EntityRepository<T>> logger)
    {
        this._dBContext = dBContext;
        this._logger = logger;
    }

    public async Task<T?> AddAsync(T item, CancellationToken cancel = default)
    {
        item.Id = default;
        await this._dBContext
            .Set<T>()
            .AddAsync(item, cancel)
            .ConfigureAwait(false);
        await this._dBContext
            .SaveChangesAsync(cancel);
        this._logger.LogInformation("> > Entity added to database");
        return item;
    }

    public async Task<int> CountAsync(CancellationToken cancel = default)
    {
        var result = await this._dBContext
            .Set<T>()
            .CountAsync(cancel)
            .ConfigureAwait(false);
        this._logger.LogInformation("> > Number of entries received");
        return result;
    }

    public async Task<T?> DeleteAsync(T item, CancellationToken cancel = default)
    {
        if (!await this._dBContext.Set<T>().AnyAsync(i => i.Id == item.Id, cancel).ConfigureAwait(false))
        {
            this._logger.LogInformation("> > Entity not found in database");
            return null;
        }

        this._dBContext.Set<T>().Remove(item);
        await this._dBContext
            .SaveChangesAsync(cancel);
        this._logger.LogInformation("> > Entity removed from database");
        return item;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default)
    {
        var items = await _dBContext
            .Set<T>()
            .ToArrayAsync(cancel)
            .ConfigureAwait(false);
        this._logger.LogInformation("> > Received all the data");
        return items;
    }

    public async Task<T?> GetByIDAsync(int id, CancellationToken cancel = default)
    {
        var item = await this._dBContext
            .Set<T>()
            .FirstOrDefaultAsync(x => x.Id == id, cancel)
            .ConfigureAwait(false);
        this._logger.LogInformation("> > Received data by ID");
        return item;
    }

    public async Task<bool> UpdateAsync(T item, CancellationToken cancel = default)
    {
        if (!await this._dBContext.Set<T>().AnyAsync(i => i.Id == item.Id, cancel).ConfigureAwait(false))
        {
            this._logger.LogInformation("> > Entity not found in database");
            return false;
        }
        this._dBContext
            .Set<T>()
            .Update(item);
        this._logger.LogInformation("> > Entity updated in database");
        return await this._dBContext.SaveChangesAsync(cancel) > 0;

    }
}
