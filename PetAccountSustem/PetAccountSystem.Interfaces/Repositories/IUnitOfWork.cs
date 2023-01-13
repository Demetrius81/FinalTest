namespace PetAccountSystem.Interfaces.Repositories;

public interface IUnitOfWork
{
    bool SaveChanges();
    Task<bool> SaveChangesAsync();
}
