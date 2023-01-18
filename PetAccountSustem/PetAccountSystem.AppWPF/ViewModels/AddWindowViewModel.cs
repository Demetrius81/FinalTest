using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class AddWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;
    private readonly ILogic? _logic;

    private Dictionary<string, Pet> _petsDictionary = new();

    #region KindOfPets

    private ICollection<string> _kindOfPets = new List<string>() { string.Empty };

    /// <summary>Список видов питомцев</summary>
    public ICollection<string> KindOfPets
    {
        get => _kindOfPets;
        set => Set(ref _kindOfPets, value);
    }

    #endregion

    #region SelectedKindOfPet

    private string _selectedKindOfPet = string.Empty;

    /// <summary>Статус программы</summary>
    public string SelectedKindOfPet
    {
        get => _selectedKindOfPet;
        set => Set(ref _selectedKindOfPet, value);
    }

    #endregion

    #region EnteredValue

    private string _enteredValue = string.Empty;

    /// <summary>Статус программы</summary>
    public string EnteredValue
    {
        get => _enteredValue;
        set => Set(ref _enteredValue, value);
    }

    #endregion


    #region Commands

    #region AddKindOfPetsCommand

    private LambdaCommand? _AddKindOfPetsCommand;

    public ICommand AddKindOfPetsCommand => _AddKindOfPetsCommand ??= new(OnAddKindOfPetsCommandExecuted, p => true);

    private void OnAddKindOfPetsCommandExecuted()
    {
        this._userDialog?.OpenAddKindOfPetsWindow();
        OnDialogComplete(EventArgs.Empty);

    }

    #endregion

    #region AddPetCommand

    private LambdaCommand? _AddPetCommand;

    public ICommand AddPetCommand => _AddPetCommand ??= new(OnAddPetCommandExecuted, CanAddPetCommandExecute);

    private async void OnAddPetCommandExecuted()
    {
        if (string.IsNullOrEmpty(EnteredValue))
        {
            throw new InvalidOperationException("Need something enter");
        }

        int count;
        if (!int.TryParse(EnteredValue, out count))
        {
            

            throw new InvalidOperationException("Need enter some number");
        }
        else if (count <= 0)
        {
            throw new InvalidOperationException("Number must be greater then zero");
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

    private LambdaCommand? _MainWindowCallCommand;

    public ICommand MainWindowCallCommand => _MainWindowCallCommand ??= new(OnMainWindowCallCommandExecuted, p => true);

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

    private Dictionary<string, Pet> GetPetsKind()
    {
        var temp = this._logic?.GetAllPetsAsync().Result;
        if (temp is null)
        {
            return new Dictionary<string, Pet> ();
        }

        return temp.ToDictionary(x => x.KindOfAnimal ??= string.Empty);
    }
}
