using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services.Interfaces;
using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;

/// <summary>Модель представления окна добавлени нового вида питомцев</summary>
internal class AddKindOfPetsWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;
    private readonly ILogic? _logic;

    /// <summary>Коллекция названий видов питомцев</summary>
    private IEnumerable<string?> KindsOfPet { get; set; } = new List<string>();

    #region IsPack

    /// <summary>Въючный питомец</summary>
    private bool _isPack;

    /// <summary>Въючный питомец</summary>
    public bool IsPack
    {
        get => _isPack;
        set => Set(ref _isPack, value);
    }

    #endregion

    #region EnteredValue

    /// <summary>Введенноый текст</summary>
    private string _enteredValue = string.Empty;

    /// <summary>Введенноый текст</summary>
    public string EnteredValue
    {
        get => _enteredValue;
        set => Set(ref _enteredValue, value);
    }

    #endregion

    #region Commands

    #region AddKindCommand
    /// <summary>Команда добавления вида питомца</summary>
    private LambdaCommand? _AddKindCommand;

    /// <summary>Команда добавления вида питомца</summary>
    public ICommand AddKindCommand => _AddKindCommand ??= new(OnAddKindCommandExecuted, CanAddKindCommandExecute);

    /// <summary>Логика команды добавления вида питомца</summary>
    private void OnAddKindCommandExecuted()
    {
        if (KindsOfPet.Contains(EnteredValue))
        {
            throw new InvalidOperationException($"The animal {EnteredValue} already exists");
        }

        _logic.AddNewAnimal(EnteredValue, IsPack);
        OnMainWindowCallCommandExecuted();
    }

    /// <summary>Логика возможности выполнения команды добавления вида питомца</summary>
    private bool CanAddKindCommandExecute() => EnteredValue.Length > 0;

    #endregion

    #region MainWindowCallCommand

    private LambdaCommand? _MainWindowCallCommand;

    public ICommand MainWindowCallCommand => _MainWindowCallCommand ??= new(OnMainWindowCallCommandExecuted, p => true);

    private void OnMainWindowCallCommandExecuted()
    {
        this._userDialog?.OpenMainWindow();
        OnDialogComplete(EventArgs.Empty);
    }

    #endregion

    #endregion

    public AddKindOfPetsWindowViewModel()
    {
        Title = "Добавить тип питомца";
        IsPack = false;
    }

    public AddKindOfPetsWindowViewModel(IUserDialog userDialog, ILogic logic) : this()
    {
        this._userDialog = userDialog;
        this._logic = logic;
        KindsOfPet = GetPetsKind();
    }

    private IEnumerable<string?> GetPetsKind()
    {
        var temp = this._logic?.GetPetsAsync().Result;
        if (temp is null)
        {
            return new List<string>();
        }

        return temp.Select(i => i.KindOfAnimal);
    }
}
