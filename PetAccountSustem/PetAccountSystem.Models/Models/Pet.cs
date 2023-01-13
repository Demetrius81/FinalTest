using System.ComponentModel.DataAnnotations;

namespace PetAccountSystem.Models.Models;
internal class Pet : Entity
{
    [Required]
    public string? KindOfAnimal { get; set; }

    [Required]
    public int Count { get; set; }

    [Required]
    public bool IsPackAnimal { get; set; }
}
