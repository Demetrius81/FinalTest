using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Client.Pets;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class MainWindowViewModel : TitledViewModel
{
    private readonly DomainLogic _domainLogic;
    private readonly IUserDialog _userDialog;

    #region Pets

    private ICollection<Pet> _pets = new List<Pet>() { new Pet() };

    /// <summary>Список питомцев</summary>
    public ICollection<Pet> Pets
    {
        get => _pets;
        set => Set(ref _pets, value);
    }

    #endregion
      
    #region Status

    private string _status = "Готово!";

    /// <summary>Статус программы</summary>
    public string Status
    {
        get => _status;
        set => Set(ref _status, value);
    }

    #endregion

    #region ShowAll

    private bool _showAll = false;

    /// <summary>Показать всех питомцев</summary>
    public bool ShowAll
    {
        get => _showAll;
        set => Set(ref _showAll, value);
    }

    #endregion

    #region ShowHome

    private bool _showHome;

    /// <summary>Показать домашних питомцев</summary>
    public bool ShowHome
    {
        get => _showHome;
        set => Set(ref _showHome, value);
    }

    #endregion

    #region ShowPack

    private bool _showPack;

    /// <summary>Показать въючных питомцев</summary>
    public bool ShowPack
    {
        get => _showPack;
        set => Set(ref _showPack, value);
    }

    #endregion

    #region TotalPets

    private int _totalPets;

    /// <summary>Всего питомцев</summary>
    public int TotalPets
    {
        get => _totalPets;
        set => Set(ref _totalPets, value);
    }

    #endregion

    #region Commands

    #region CloseApplicationCommand

    private LambdaCommand? _CloseApplicationCommand;

    public ICommand CloseApplicationCommand => _CloseApplicationCommand ??= new(OnCloseApplicationCommandExecuted, p => true);

    private void OnCloseApplicationCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }

    private bool CanCloseApplicationCommandExecute(object p) => true;

    #endregion

    #region SwichShowStatusCommand

    private LambdaCommand? _SwichShowStatusCommand;

    public ICommand SwichShowStatusCommand => _AddWindowCallCommand ??= new(OnSwichShowStatusCommandExecuted, p => true);

    private async void OnSwichShowStatusCommandExecuted(object p)
    {
        var temp = true switch
        {
            true when _showAll => await _domainLogic.GetPetsAsync().ConfigureAwait(true),
            true when _showHome => await _domainLogic.GetPetsHomeAsync().ConfigureAwait(true),
            true when _showPack => await _domainLogic.GetPetsPackAsync().ConfigureAwait(true),
            _ => Enumerable.Empty<Pet>()
        };

        if (temp is null || temp == Enumerable.Empty<Pet>())
        {
            TotalPets = 0;
            Pets = new List<Pet>() { new Pet() };
            Status = "Нет связи с сервером!";
            return;
        }

        TotalPets = temp.Sum(i => i.Count);
        Pets = temp.ToList();
        Status = "Готов!";
    }

    #endregion

    #region AddWindowCallCommand

    private LambdaCommand? _AddWindowCallCommand;

    public ICommand AddWindowCallCommand => _AddWindowCallCommand ??= new(OnAddWindowCallCommandExecuted, p => true);

    private void OnAddWindowCallCommandExecuted(object p)
    {
        ;
    }

    #endregion

    #region RemoveWindowCallCommand

    private LambdaCommand? _RemoveWindowCallCommand;

    public ICommand RemoveWindowCallCommand => _AddWindowCallCommand ??= new(OnRemoveWindowCallCommandExecuted, p => !(Pets == Enumerable.Empty<Pet>() || Pets.Count == 1 && (Pets.FirstOrDefault() is null || Pets.FirstOrDefault()?.Id == 0)));

    private void OnRemoveWindowCallCommandExecuted(object p)
    {
        ;
    }

    #endregion

    #endregion

    public MainWindowViewModel()
    {
        Title = "Система учета питомника";
    }

    public MainWindowViewModel(DomainLogic domainLogic, IUserDialog userDialog) : this()
    {
        this._domainLogic = domainLogic;
        this._userDialog = userDialog;
    }

    //private string GetTestContent()
    //{
    //    return CheckStatus ? "Checked" : "Unchecked";
    //}



}
