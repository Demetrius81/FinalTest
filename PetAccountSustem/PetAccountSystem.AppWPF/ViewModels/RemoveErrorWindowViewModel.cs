using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class RemoveErrorWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;

    #region RemoveWindowCallCommand

    private LambdaCommand? _RemoveWindowCallCommand;

    public ICommand RemoveWindowCallCommand =>
        _RemoveWindowCallCommand ??= new(OnRemoveWindowCallCommandExecuted, prop => true);

    private void OnRemoveWindowCallCommandExecuted()
    {
        this._userDialog?.OpenRemoveWindow();
        OnDialogComplete(EventArgs.Empty);
    }

    #endregion

    public RemoveErrorWindowViewModel() { }

    public RemoveErrorWindowViewModel(IUserDialog userDialog) : this()
    {
        this._userDialog = userDialog;
    }
}
