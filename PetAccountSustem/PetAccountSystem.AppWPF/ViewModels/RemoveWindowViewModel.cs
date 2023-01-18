using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class RemoveWindowViewModel : DialogViewModel
{
    private readonly IUserDialog _userDialog;
    private readonly ILogic _logic;

    private Dictionary<string, Pet> _petsDictionary = new();

    #region KindsOfPets

    private ICollection<string?> _kindsOfPets = new List<string?>() { string.Empty };

    /// <summary>Список видов питомцев</summary>
    public ICollection<string?> KindsOfPets
    {
        get => _kindsOfPets;
        set => Set(ref _kindsOfPets, value);
    }

    #endregion

    #region SelectedKindOfPet

    private string _selectedKindOfPet = string.Empty;

    /// <summary>Статус программы</summary>
    public string SelectedKindOfPet
    {
        get => _selectedKindOfPet;
        set
        {
            Set(ref _selectedKindOfPet, value);
            CountofPets = _petsDictionary[_selectedKindOfPet].Count;
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

        _petsDictionary[SelectedKindOfPet] = await _logic.RemoveUpdatePetsCount(count, pet).ConfigureAwait(true);
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

    public RemoveWindowViewModel(IUserDialog userDialog, ILogic logic)
    {
        this._logic = logic;
        this._userDialog = userDialog;
        _petsDictionary = GetPetsKind();

        //_petsDictionary.Add("aaa", new Pet() { Id = 1, KindOfAnimal = "aaa", Count = 4 });
        //_petsDictionary.Add("bbb", new Pet() { Id = 2, KindOfAnimal = "bbb", Count = 5 });
        //_petsDictionary.Add("ccc", new Pet() { Id = 3, KindOfAnimal = "ccc", Count = 6 });

        KindsOfPets = _petsDictionary.Keys.ToList() ?? new List<string>() { string.Empty };
    }

    private Dictionary<string, Pet> GetPetsKind()
    {
        var temp = this._logic?.GetPetsAsync().Result;
        if (temp is null)
        {
            return new Dictionary<string, Pet>();
        }

        return temp.ToDictionary(x => x.KindOfAnimal ??= string.Empty);
    }
}
