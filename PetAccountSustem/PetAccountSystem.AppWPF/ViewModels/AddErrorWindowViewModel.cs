using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services.Interfaces;
using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;

/// <summary>Модель представления окна ошибки</summary>
internal class AddErrorWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;

    #region AddWindowCallCommand

    /// <summary>Команда вызова окна добавления</summary>
    private LambdaCommand? _AddWindowCallCommand;

    /// <summary>Команда вызова окна добавления</summary>
    public ICommand AddWindowCallCommand =>
        _AddWindowCallCommand ??= new(OnAddWindowCallCommandExecuted, prop => true);

    /// <summary>Логика команды вызова окна добавления</summary>
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
