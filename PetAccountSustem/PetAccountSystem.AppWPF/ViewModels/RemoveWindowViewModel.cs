using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services.Interfaces;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;

/// <summary>Модель представления окна удаления питомцев</summary>
internal class RemoveWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;
    private readonly ILogic? _logic;

    /// <summary>Словарь название питомца - питомец</summary>
    private readonly Dictionary<string, Pet> _petsDictionary = new();

    #region KindsOfPets

    /// <summary>Коллекция названий питомцев</summary>
    private ICollection<string> _kindsOfPets = new List<string>() { string.Empty };

    /// <summary>Коллекция названий питомцев</summary>
    public ICollection<string> KindsOfPets
    {
        get => _kindsOfPets;
        set => Set(ref _kindsOfPets, value);
    }

    #endregion

    #region SelectedKindOfPet

    /// <summary>Выбранное название питомца</summary>
    private string _selectedKindOfPet = string.Empty;

    /// <summary>Выбранное название питомца</summary>
    public string SelectedKindOfPet
    {
        get => _selectedKindOfPet;
        set
        {
            Set(ref _selectedKindOfPet, value);
            CountofPets = this._petsDictionary[_selectedKindOfPet].Count;
        }
    }

    #endregion

    #region CountofPets

    private int _countofPets;

    /// <summary>Статус программы</summary>
    public int CountofPets
    {
        get => _countofPets;
        set => Set(ref _countofPets, value);
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

    #region RemovePetCommand

    /// <summary>Команда удаления питомцев</summary>
    private LambdaCommand? _RemovePetCommand;

    /// <summary>Команда удаления питомцев</summary>
    public ICommand RemovePetCommand => _RemovePetCommand ??= new(OnRemovePetCommandExecuted, CanRemovePetCommandExecute);

    /// <summary>Логика команды удаления питомцев</summary>
    private async void OnRemovePetCommandExecuted()
    {
        if (string.IsNullOrEmpty(EnteredValue) || !int.TryParse(EnteredValue, out int count) || count <= 0)
        {
            this._userDialog?.OpenRemoveErrorWindow();
            OnDialogComplete(EventArgs.Empty);
            return;
        }

        var result = this._petsDictionary.TryGetValue(SelectedKindOfPet, out Pet? pet);
        if (!result || pet is null)
        {
            throw new InvalidOperationException("Something wrong with logic");
        }

        this._petsDictionary[SelectedKindOfPet] = await this._logic.RemoveUpdatePetsCount(count, pet).ConfigureAwait(true);
        OnMainWindowCallCommandExecuted();
    }

    /// <summary>Логика проверки возможности выполнения команды удаления питомцев</summary>
    private bool CanRemovePetCommandExecute() => CountofPets > 0 && EnteredValue.Length > 0;

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

    public RemoveWindowViewModel()
    {
        Title = "Удалить питомца";
    }

    public RemoveWindowViewModel(IUserDialog userDialog, ILogic logic)
    {
        this._logic = logic;
        this._userDialog = userDialog;
        this._petsDictionary = GetPetsKind();
        KindsOfPets = this._petsDictionary.Keys.ToList() ?? new List<string>() { string.Empty };
    }

    /// <summary>Метод заполнения коллекций питомцами</summary>
    /// <returns>Словарь питомцев</returns>
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
