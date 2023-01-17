using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class RemoveWindowViewModel : DialogViewModel
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

    #region RemovePetCommand

    private LambdaCommand? _RemovePetCommand;

    public ICommand RemovePetCommand => _RemovePetCommand ??= new(OnRemovePetCommandExecuted, p => true);

    private async void OnRemovePetCommandExecuted()
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
    public RemoveWindowViewModel()
    {
        Title = "Удалить питомца";
    }

    public RemoveWindowViewModel(IUserDialog userDialog, DomainLogic logic)
    {
        this._logic = logic;
        this._userDialog = userDialog;
    }
}
