using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services.Interfaces;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;

/// <summary>Модель представления окна добавлени питомцев</summary>
internal class AddWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;
    private readonly ILogic? _logic;

    /// <summary>Словарь название питомца - питомец</summary>
    private readonly Dictionary<string, Pet> _petsDictionary = new();

    #region KindOfPets

    /// <summary>Коллекция названий питомцев</summary>
    private ICollection<string> _kindOfPets = new List<string>() { string.Empty };

    /// <summary>Коллекция названий питомцев</summary>
    public ICollection<string> KindOfPets
    {
        get => _kindOfPets;
        set => Set(ref _kindOfPets, value);
    }

    #endregion

    #region SelectedKindOfPet

    /// <summary>Выбранное название питомца</summary>
    private string _selectedKindOfPet = string.Empty;

    /// <summary>Выбранное название питомца</summary>
    public string SelectedKindOfPet
    {
        get => _selectedKindOfPet;
        set => Set(ref _selectedKindOfPet, value);
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

    #region AddPetCommand

    /// <summary>Команда добавления новых питомцев</summary>
    private LambdaCommand? _AddPetCommand;

    /// <summary>Команда добавления новых питомцев</summary>
    public ICommand AddPetCommand => _AddPetCommand ??= new(OnAddPetCommandExecuted, CanAddPetCommandExecute);

    /// <summary>Логика команды добавления новых питомцев</summary>
    private async void OnAddPetCommandExecuted()
    {
        if (string.IsNullOrEmpty(EnteredValue) ||
            !int.TryParse(EnteredValue, out int count) ||
            count <= 0)
        {
            this._userDialog?.OpenAddErrorWindow();
            OnDialogComplete(EventArgs.Empty);
            return;
        }

        var result = this._petsDictionary.TryGetValue(SelectedKindOfPet, out Pet? pet);

        if (!result || pet is null)
        {
            throw new InvalidOperationException("Something wrong with logic");
        }

        this._petsDictionary[SelectedKindOfPet] = await this._logic.AddUpdatePetsCount(count, pet).ConfigureAwait(true);
        OnMainWindowCallCommandExecuted();
    }

    private bool CanAddPetCommandExecute() => !string.IsNullOrWhiteSpace(SelectedKindOfPet) && EnteredValue.Length > 0;

    #endregion

    #region MainWindowCallCommand

    /// <summary>Команда вызова главного окна</summary>
    private LambdaCommand? _MainWindowCallCommand;

    /// <summary>Команда вызова главного окна</summary>
    public ICommand MainWindowCallCommand => _MainWindowCallCommand ??= new(OnMainWindowCallCommandExecuted, p => true);

    /// <summary>Логика команды вызова главного окна</summary>
    private void OnMainWindowCallCommandExecuted()
    {
        this._userDialog?.OpenMainWindow();
        OnDialogComplete(EventArgs.Empty);
    }

    #endregion

    #endregion

    public AddWindowViewModel()
    {
        Title = "Добавить питомца";
    }

    public AddWindowViewModel(IUserDialog userDialog, ILogic logic) : this()
    {
        this._userDialog = userDialog;
        this._logic = logic;
        _petsDictionary = GetPetsKind();
        KindOfPets = _petsDictionary.Keys.ToList() ?? new List<string>() { string.Empty };
    }

    /// <summary>Метод заполнения коллекций питомцами</summary>
    /// <returns>Коллекция питомцев</returns>
    private Dictionary<string, Pet> GetPetsKind()
    {
        var temp = this._logic?.GetAllPetsAsync().Result;
        if (temp is null)
        {
            return new Dictionary<string, Pet>();
        }

        return temp.ToDictionary(x => x.KindOfAnimal ??= string.Empty);
    }
}
