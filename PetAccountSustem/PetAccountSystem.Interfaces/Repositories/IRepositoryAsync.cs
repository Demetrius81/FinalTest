using PetAccountSystem.Interfaces.Entities;

namespace PetAccountSystem.Interfaces.Repositories;

public interface IRepositoryAsync<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIDAsync(int id);
    Task<int> CountAsync();
    Task<T?> AddAsync(T item);
    Task<bool> UpdateAsync(T item);
    Task<bool> DeleteAsync(T item);
}
