using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Models;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class AddWindowViewModel : DialogViewModel
{
    private readonly IUserDialog _userDialog;
    private readonly DomainLogic _logic;

    #region Commands

    #region AddKindOfPetsCommand

    private LambdaCommand? _AddKindOfPetsCommand;

    public ICommand AddKindOfPetsCommand => _AddKindOfPetsCommand ??= new(OnAddKindOfPetsCommandExecuted, p => true);

    private async void OnAddKindOfPetsCommandExecuted(object p)
    {
        var temp = await _logic.GetPetsAsync().ConfigureAwait(true);

        if (temp is null || temp == Enumerable.Empty<Pet>())
        {
            return;
        }

        var nexttemp = temp.Select(i => i.KindOfAnimal);

    }

    #endregion

    #region AddPetCommand

    private LambdaCommand? _AddPetCommand;

    public ICommand AddPetCommand => _AddPetCommand ??= new(OnAddPetCommandExecuted, p => true);

    private async void OnAddPetCommandExecuted()
    {
        var temp = await _logic.GetPetsAsync().ConfigureAwait(true);

        if (temp is null || temp == Enumerable.Empty<Pet>())
        {
            return;
        }

        var nexttemp = temp.Select(i => i.KindOfAnimal);
    }

    #endregion

    #region MainWindowCallCommand

    private LambdaCommand? _MainWindowCallCommand;

    public ICommand MainWindowCallCommand => _MainWindowCallCommand ??= new(OnMainWindowCallCommandExecuted, p => true);

    private void OnMainWindowCallCommandExecuted()
    {
        _userDialog.OpenMainWindow();
        OnDialogComplete(EventArgs.Empty);
    }

    #endregion

    #endregion

    public AddWindowViewModel()
	{
		Title = "Добавить питомца";
	}

    public AddWindowViewModel(IUserDialog userDialog, DomainLogic logic) : this()
    {
        this._userDialog = userDialog;
        this._logic = logic;
    }
}
