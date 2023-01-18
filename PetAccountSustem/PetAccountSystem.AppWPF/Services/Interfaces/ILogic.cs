using PetAccountSystem.Models.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PetAccountSystem.AppWPF.Services.Interfaces;
internal interface ILogic
{
    Task<IEnumerable<Pet>> GetAllPetsAsync(CancellationToken cancel = default);
    Task<bool> AddNewAnimal(string kindOfPet, bool isPack, CancellationToken cancel = default);
    Task<Pet> AddUpdatePetsCount(int count, Pet pet, CancellationToken cancel = default);
    Task<IEnumerable<Pet>> GetPetsAsync(CancellationToken cancel = default);
    Task<IEnumerable<Pet>> GetPetsHomeAsync(CancellationToken cancel = default);
    Task<IEnumerable<Pet>> GetPetsPackAsync(CancellationToken cancel = default);
    Task<Pet> RemoveUpdatePetsCount(int count, Pet pet, CancellationToken cancel = default);
}