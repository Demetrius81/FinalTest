using PetAccountSystem.Models.Models;
using System.Collections.Generic;

namespace PetAccountSystem.AppWPF.Models;
public class Pets
{
    public ICollection<Pet>? PetList { get; set; }
}
