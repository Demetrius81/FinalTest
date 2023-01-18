using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class AddErrorWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;

    #region AddWindowCallCommand

    private LambdaCommand? _AddWindowCallCommand;

    public ICommand AddWindowCallCommand =>
        _AddWindowCallCommand ??= new(OnAddWindowCallCommandExecuted, prop => true);

    private void OnAddWindowCallCommandExecuted()
    {
        _userDialog?.OpenAddWindow();
        OnDialogComplete(EventArgs.Empty);
    }

    #endregion

    public AddErrorWindowViewModel() { }

    public AddErrorWindowViewModel(IUserDialog userDialog) : this()
    {
        this._userDialog = userDialog;
    }
}
