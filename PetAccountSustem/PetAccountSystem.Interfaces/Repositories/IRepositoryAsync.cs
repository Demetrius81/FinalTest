using PetAccountSystem.Interfaces.Entities;

namespace PetAccountSystem.Interfaces.Repositories;

/// <summary>Базовое понятие асинхронного репозитория</summary>
/// <typeparam name="T">Сущность</typeparam>
public interface IRepositoryAsync<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default);
    Task<T?> GetByIDAsync(int id, CancellationToken cancel = default);
    Task<int> CountAsync(CancellationToken cancel = default);
    Task<T?> AddAsync(T item, CancellationToken cancel = default);
    Task<bool> UpdateAsync(T item, CancellationToken cancel = default);
    Task<T?> DeleteAsync(T item, CancellationToken cancel = default);
}
