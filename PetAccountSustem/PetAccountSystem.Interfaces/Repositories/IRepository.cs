using PetAccountSystem.Interfaces.Entities;

namespace PetAccountSystem.Interfaces.Repositories;

/// <summary>Базовое понятие репозитория</summary>
/// <typeparam name="T">Сущность</typeparam>
public interface IRepository<T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T? GetByID(int id);
    int Count();
    int Add(T item);
    bool Update(T item);
    T? Delete(T item);
}
