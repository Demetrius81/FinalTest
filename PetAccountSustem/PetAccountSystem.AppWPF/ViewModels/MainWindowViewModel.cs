﻿using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.Services.Interfaces;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class MainWindowViewModel : DialogViewModel
{
    private readonly ILogic? _domainLogic;
    private readonly IUserDialog? _userDialog;

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

    private string _status = string.Empty;

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

    public ICommand CloseApplicationCommand =>
        _CloseApplicationCommand ??= new(OnCloseApplicationCommandExecuted, p => true);

    private void OnCloseApplicationCommandExecuted()
    {
        Application.Current.Shutdown();
    }

    #endregion

    #region SwichShowStatusCommand

    private LambdaCommand? _SwichShowStatusCommand;

    public ICommand SwichShowStatusCommand =>
        _SwichShowStatusCommand ??= new(OnSwichShowStatusCommandExecuted, p => true);

    private async void OnSwichShowStatusCommandExecuted()
    {
        var temp = true switch
        {
            true when _showAll => await this._domainLogic.GetPetsAsync().ConfigureAwait(true),
            true when _showHome => await this._domainLogic.GetPetsHomeAsync().ConfigureAwait(true),
            true when _showPack => await this._domainLogic.GetPetsPackAsync().ConfigureAwait(true),
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

    public ICommand AddWindowCallCommand =>
        _AddWindowCallCommand ??= new(OnAddWindowCallCommandExecuted, CanAddWindowCallCommandExecute);

    private void OnAddWindowCallCommandExecuted()
    {
        this._userDialog?.OpenAddWindow();
        OnDialogComplete(EventArgs.Empty);
    }

    private bool CanAddWindowCallCommandExecute() => Status != "Нет связи с сервером!";

    #endregion

    #region RemoveWindowCallCommand

    private LambdaCommand? _RemoveWindowCallCommand;

    public ICommand RemoveWindowCallCommand =>
        _RemoveWindowCallCommand ??= new(OnRemoveWindowCallCommandExecuted, CanRemoveWindowCallCommandExecute);

    private void OnRemoveWindowCallCommandExecuted()
    {
        this._userDialog?.OpenRemoveWindow();
        OnDialogComplete(EventArgs.Empty);
    }

    private bool CanRemoveWindowCallCommandExecute() =>
        _status != "Нет связи с сервером!" ||
        Pets != Enumerable.Empty<Pet>() ||
        Pets.Count != 1 && (Pets.FirstOrDefault() is not null || Pets.FirstOrDefault()?.Id != 0);

    #endregion

    #endregion

    public MainWindowViewModel()
    {
        Title = "Система учета питомника";
        Status = "Нет связи с сервером!";
    }

    public MainWindowViewModel(ILogic domainLogic, IUserDialog userDialog) : this()
    {
        this._domainLogic = domainLogic;
        this._userDialog = userDialog;
        SwitchChoiseOnShowAll();
    }


    private void SwitchChoiseOnShowAll()
    {
        ShowAll = true;
        OnSwichShowStatusCommandExecuted();
    }


    public void UpdateStatus()
    {
        Status = "Нет связи с сервером!";
        OnSwichShowStatusCommandExecuted();
    }



}
