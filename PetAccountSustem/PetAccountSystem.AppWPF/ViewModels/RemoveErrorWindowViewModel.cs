using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services.Interfaces;
using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;

/// <summary>Модель представления окна ошибки</summary>
internal class RemoveErrorWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;

    #region RemoveWindowCallCommand

    /// <summary>Команда вызова окна удаления</summary>
    private LambdaCommand? _RemoveWindowCallCommand;

    /// <summary>Команда вызова окна удаления</summary>
    public ICommand RemoveWindowCallCommand =>
        _RemoveWindowCallCommand ??= new(OnRemoveWindowCallCommandExecuted, prop => true);

    /// <summary>Логика команды вызова окна удаления</summary>
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
