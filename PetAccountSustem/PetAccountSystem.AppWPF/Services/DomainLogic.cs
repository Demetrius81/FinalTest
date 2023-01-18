using PetAccountSystem.Client.Pets;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetAccountSystem.AppWPF.Services;
internal class DomainLogic : ILogic
{
    private const string BASE_ADDRESS = "http://localhost:5241";

    private readonly PetsClient _petsClient;

    public DomainLogic()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(BASE_ADDRESS);
        this._petsClient = new PetsClient(client);
    }

    public async Task<IEnumerable<Pet>> GetPetsAsync(CancellationToken cancel = default)
    {
        IEnumerable<Pet>? result = null;
        try
        {
            result = await _petsClient.GetAllAsync(cancel).ConfigureAwait(false);
            result = result.Where(x => x.Count > 0);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }

        return result ?? Enumerable.Empty<Pet>();
    }

    public async Task<IEnumerable<Pet>> GetPetsHomeAsync(CancellationToken cancel = default)
    {
        IEnumerable<Pet>? result = null;
        try
        {
            result = await _petsClient.GetAllAsync(cancel).ConfigureAwait(false);
            result = result.Where(i => !i.IsPackAnimal);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }

        return result ?? Enumerable.Empty<Pet>();
    }

    public async Task<IEnumerable<Pet>> GetPetsPackAsync(CancellationToken cancel = default)
    {
        IEnumerable<Pet>? result = null;
        try
        {
            result = await _petsClient.GetAllAsync(cancel).ConfigureAwait(false);
            result = result.Where(i => i.IsPackAnimal);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }

        return result ?? Enumerable.Empty<Pet>();
    }

    public async Task<Pet> AddUpdatePetsCount(int count, Pet pet, CancellationToken cancel = default)
    {
        var petFromDb = await _petsClient.GetByIDAsync(pet.Id, cancel).ConfigureAwait(false);

        if (petFromDb is null)
        {
            throw new ArgumentException("In database current pet nit found", nameof(pet));
        }

        petFromDb.Count += count;

        if (!await _petsClient.UpdateAsync(petFromDb, cancel).ConfigureAwait(false))
        {
            throw new InvalidOperationException("Database not response");
        }

        return petFromDb;
    }

    public async Task<Pet> RemoveUpdatePetsCount(int count, Pet pet, CancellationToken cancel = default)
    {
        var petFromDb = await _petsClient.GetByIDAsync(pet.Id, cancel).ConfigureAwait(false);

        if (petFromDb is null)
        {
            throw new ArgumentException("In database current pet nit found", nameof(pet));
        }

        if (petFromDb.Count >= count)
        {
            petFromDb.Count -= count;
        }
        else
        {
            throw new InvalidOperationException("Can't remove more than there is");
        }

        if (!await _petsClient.UpdateAsync(petFromDb, cancel).ConfigureAwait(false))
        {
            throw new InvalidOperationException("Database not response");
        }

        return petFromDb;
    }

    public async Task<bool> AddNewAnimal(string kindOfPet, bool isPack, CancellationToken cancel = default)
    {
        Pet pet = new()
        {
            Id = default,
            IsPackAnimal = isPack,
            KindOfAnimal = kindOfPet,
            Count = 0,
        };

        var result = await this._petsClient.AddAsync(pet, cancel).ConfigureAwait(false);
        return result is not null;
    }
}
