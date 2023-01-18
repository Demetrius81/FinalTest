using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class AddKindErrorWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;

    #region AddKindOfPetsCommand

    private LambdaCommand? _AddKindOfPetsCommand;

    public ICommand AddKindOfPetsCommand => _AddKindOfPetsCommand ??= new(OnAddKindOfPetsCommandExecuted, p => true);

    private void OnAddKindOfPetsCommandExecuted()
    {
        this._userDialog?.OpenAddKindOfPetsWindow();
        OnDialogComplete(EventArgs.Empty);

    }

    #endregion

    public AddKindErrorWindowViewModel() { }

    public AddKindErrorWindowViewModel(IUserDialog userDialog) : this()
    {
        this._userDialog = userDialog;
    }
}
