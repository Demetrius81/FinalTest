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
internal class AddKindOfPetsWindowViewModel : DialogViewModel
{
    private readonly IUserDialog? _userDialog;
    private readonly ILogic? _logic;

    private IEnumerable<string?> KindsOfPet { get; set; }

    #region IsPack

    private bool _isPack;

    /// <summary>Статус программы</summary>
    public bool IsPack
    {
        get => _isPack;
        set => Set(ref _isPack, value);
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

    #region AddKindCommand

    private LambdaCommand? _AddKindCommand;

    public ICommand AddKindCommand => _AddKindCommand ??= new(OnAddKindCommandExecuted, CanAddKindCommandExecute);

    private void OnAddKindCommandExecuted()
    {
        if (KindsOfPet.Contains(EnteredValue))
        {
            throw new InvalidOperationException($"The animal {EnteredValue} already exists");
        }

        _logic.AddNewAnimal(EnteredValue, IsPack);
    }

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
