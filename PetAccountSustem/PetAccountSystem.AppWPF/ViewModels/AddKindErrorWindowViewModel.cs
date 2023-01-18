using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services.Interfaces;
using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;

/// <summary>Модель представления окна ошибки</summary>
internal class AddKindErrorWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;

    #region AddKindOfPetsCommand

    /// <summary>Команда вызова окна добавления нового вида питомца</summary>
    private LambdaCommand? _AddKindOfPetsCommand;

    /// <summary>Команда вызова окна добавления нового вида питомца</summary>
    public ICommand AddKindOfPetsCommand => _AddKindOfPetsCommand ??= new(OnAddKindOfPetsCommandExecuted, p => true);

    /// <summary>Логика команды вызова окна добавления нового вида питомца</summary>
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
