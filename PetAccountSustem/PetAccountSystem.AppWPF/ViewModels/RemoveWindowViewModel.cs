using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class RemoveWindowViewModel : DialogViewModel
{
    private readonly IUserDialog _userDialog;
    private readonly DomainLogic _logic;


    private Dictionary<string, Pet> _petsDictionary = new();

    #region Pets

    private ICollection<Pet> _pets = new List<Pet>() { new Pet() };

    /// <summary>Список питомцев</summary>
    public ICollection<Pet> Pets
    {
        get => _pets;
        set => Set(ref _pets, value);
    }

    #endregion

    #region KindOfPets

    private ICollection<string?> _kindOfPets = new List<string?>() { string.Empty };

    /// <summary>Список видов питомцев</summary>
    public ICollection<string?> KindOfPets
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

    #region RemovePetCommand

    private LambdaCommand? _RemovePetCommand;

    public ICommand RemovePetCommand => _RemovePetCommand ??= new(OnRemovePetCommandExecuted, CanRemovePetCommandExecute);

    private async void OnRemovePetCommandExecuted()
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

        var result = _petsDictionary.TryGetValue(SelectedKindOfPet, out Pet? pet);

        if (!result || pet is null)
        {
            throw new InvalidOperationException("Something wrong with logic");
        }

        _petsDictionary[SelectedKindOfPet] = await _logic.AddUpdatePetsCount(count, pet).ConfigureAwait(true);
    }

    private bool CanRemovePetCommandExecute() => !string.IsNullOrWhiteSpace(SelectedKindOfPet) && EnteredValue.Length > 0;

    #endregion

    #region MainWindowCallCommand

    private LambdaCommand? _MainWindowCallCommand;

    public ICommand MainWindowCallCommand => _MainWindowCallCommand ??= new(OnMainWindowCallCommandExecuted, p => true);

    private void OnMainWindowCallCommandExecuted()
    {
        _userDialog.OpenMainWindow();
        OnDialogComplete(EventArgs.Empty);
    }

    #endregion

    #endregion

    public RemoveWindowViewModel()
    {
        Title = "Удалить питомца";
    }

    public RemoveWindowViewModel(IUserDialog userDialog, DomainLogic logic)
    {
        this._logic = logic;
        this._userDialog = userDialog;
    }
}
