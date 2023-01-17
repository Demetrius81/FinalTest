using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class RemoveWindowViewModel : DialogViewModel
{
	public RemoveWindowViewModel()
	{
		Title = "Удалить питомца";
	}
}
