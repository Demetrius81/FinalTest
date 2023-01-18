using System.ComponentModel.DataAnnotations;

namespace PetAccountSystem.Models.Models;

/// <summary>Класс - модель питомца</summary>
public class Pet : Entity
{
    /// <summary>Вид питомца</summary>
    public string? KindOfAnimal { get; set; }

    /// <summary>Количество питомцев данного вида</summary>
    public int Count { get; set; }

    /// <summary>Флаг питомец въючный или домашний</summary>
    public bool IsPackAnimal { get; set; }
}
