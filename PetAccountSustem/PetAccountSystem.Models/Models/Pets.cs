using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAccountSystem.Models.Models;
public class Pets
{
    public int Id { get; set; }
    public ICollection<Pet>? PetList { get; set; }
}
