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

    public ICommand CloseApplicationCommand { get; }

    private void OnCloseApplicationCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }

    private bool CanCloseApplicationCommandExecute(object p) => true;

    #endregion

    #region SwichShowStatusCommand

    public ICommand SwichShowStatusCommand { get; }

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

    private bool CanSwichShowStatusCommandExecute(object p) => true;

    #endregion

    #region AddWindowCallCommand

    public ICommand AddWindowCallCommand { get; }

    private void OnAddWindowCallCommandExecuted(object p)
    {
        ;
    }

    private bool CanAddWindowCallCommandExecute(object p) => true;

    #endregion

    #region RemoveWindowCallCommand

    public ICommand RemoveWindowCallCommand { get; }

    private void OnRemoveWindowCallCommandExecuted(object p)
    {
        ;
    }

    private bool CanRemoveWindowCallCommandExecute(object p)
    {
        if (Pets == Enumerable.Empty<Pet>() || Pets.Count == 1 && (Pets.FirstOrDefault() is null || Pets.FirstOrDefault()?.Id == 0))
        {
            return false;
        }

        return true;
    } 

    #endregion

    #endregion

    public MainWindowViewModel(DomainLogic domainLogic)
    {
        Title = "Система учета питомника";

        this._domainLogic = domainLogic;

        #region Commands

        CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
        SwichShowStatusCommand = new LambdaCommand(OnSwichShowStatusCommandExecuted, CanSwichShowStatusCommandExecute);
        AddWindowCallCommand = new LambdaCommand(OnAddWindowCallCommandExecuted, CanAddWindowCallCommandExecute);
        RemoveWindowCallCommand = new LambdaCommand(OnRemoveWindowCallCommandExecuted, CanRemoveWindowCallCommandExecute);

        #endregion

    }

    //private string GetTestContent()
    //{
    //    return CheckStatus ? "Checked" : "Unchecked";
    //}



}
