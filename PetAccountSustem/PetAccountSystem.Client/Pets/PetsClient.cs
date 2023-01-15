using Microsoft.Extensions.Logging;
using PetAccountSystem.Interfaces.Repositories;
using PetAccountSystem.Models.Models;
using System.Net.Http.Json;

namespace PetAccountSystem.Client.Pets;
public class PetsClient : IRepositoryAsync<Pet>
{
    private readonly HttpClient _client;
    private readonly ILogger<PetsClient>? _logger;

    public PetsClient(HttpClient client, ILogger<PetsClient>? logger = null)
    {
        this._client = client;
        this._logger = logger;
    }

    public async Task<Pet?> AddAsync(Pet item, CancellationToken cancel = default)
    {
        var result = await this._client.PutAsJsonAsync<Pet>("api/Pets/add", item, cancel).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            this._logger?.LogInformation($"> Status code is {result.StatusCode.ToString()}");
            return null;
        }

        this._logger?.LogInformation("> Item added");
        return await result.Content.ReadFromJsonAsync<Pet>(cancellationToken: cancel);
    }

    public async Task<int> CountAsync(CancellationToken cancel = default)
    {
        var result = await this._client
            .GetFromJsonAsync<int>("api/Pets/count", cancel)
            .ConfigureAwait(false);
        this._logger?.LogInformation("> Count recived");
        return result;
    }

    public async Task<Pet?> DeleteAsync(Pet item, CancellationToken cancel = default)
    {
        var result = await this._client
            .PostAsJsonAsync<Pet>("api/Pets/delete", item, cancel)
            .ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            this._logger?.LogInformation($"> Status code is {result.StatusCode.ToString()}");
            return null;
        }

        this._logger?.LogInformation("> Item removed");
        return await result.Content.ReadFromJsonAsync<Pet>(cancellationToken: cancel);
    }

    public async Task<IEnumerable<Pet>> GetAllAsync(CancellationToken cancel = default)
    {
        var pets = await this._client
            .GetFromJsonAsync<IEnumerable<Pet>>("api/Pets/getall", cancel)
            .ConfigureAwait(false);
        if (pets is null)
        {
            throw new InvalidOperationException("Failed to get data from service");
        }

        this._logger?.LogInformation("> All items recived");
        return pets;
    }

    public async Task<Pet?> GetByIDAsync(int id, CancellationToken cancel = default)
    {
        var pet = await this._client
            .GetFromJsonAsync<Pet?>($"api/Pets/getbyid?id={id}", cancel)
            .ConfigureAwait(false);
        this._logger?.LogInformation("> Item recived");
        return pet;
    }

    public async Task<bool> UpdateAsync(Pet item, CancellationToken cancel = default)
    {
        var result = await this._client
            .PostAsJsonAsync<Pet>("api/Pets/update", item, cancel)
            .ConfigureAwait(false);
        this._logger?.LogInformation("> Item updated");
        return await result.Content.ReadFromJsonAsync<bool>(cancellationToken: cancel);
    }
}
