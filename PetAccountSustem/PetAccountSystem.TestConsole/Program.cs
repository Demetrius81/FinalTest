using Microsoft.Extensions.Logging;
using PetAccountSystem.Client.Pets;

namespace PetAccountSystem.TestConsole;

internal class Program
{
    protected Program() { }

    private static void Main(string[] args)
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri("http://localhost:5241");

        var petClient = new PetsClient(client);

        var all = petClient.GetAllAsync().Result;

        var byId = petClient.GetByIDAsync(4).Result;

        var count = petClient.CountAsync().Result;

        var delete = petClient.DeleteAsync(byId).Result;

        all = petClient.GetAllAsync().Result;

        byId = petClient.GetByIDAsync(1).Result;

        byId.Count = 9999;

        var update = petClient.UpdateAsync(byId).Result;

        byId = petClient.GetByIDAsync(1).Result;

        byId.KindOfAnimal = "FFFFFFFFFFFF";

        var added = petClient.AddAsync(byId).Result;

        all = petClient.GetAllAsync().Result;

        Console.ReadKey(true);
    }
}