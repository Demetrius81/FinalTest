using System.ComponentModel.DataAnnotations;

namespace PetAccountSystem.Models.Models;
public class Pet : Entity
{
    public string? KindOfAnimal { get; set; }

    public int Count { get; set; }

    public bool IsPackAnimal { get; set; }
}
