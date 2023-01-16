using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.ViewModels.Base;
using PetAccountSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class MainWindowViewModel : ViewModel
{
    public ObservableCollection<Pet> Pets { get; set; } = new ObservableCollection<Pet>()
    {
        new Pet()
        {
            Id = 1,
            KindOfAnimal = "Horse",
            IsPackAnimal = true,
            Count = 5,
        },
        new Pet()
        {
            Id = 2,
            KindOfAnimal = "Cat",
            IsPackAnimal = false,
            Count = 7,
        }
    };

    #region Title

    private string _title = "Система учета питомника";

    /// <summary>Заголовок окна</summary>
    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
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

    //#region CheckStatus

    //private bool _checkStatus;

    ///// <summary>Статус программы</summary>
    //public bool CheckStatus
    //{
    //    get => _checkStatus;
    //    set => Set(ref _checkStatus, value);
    //}

    //#endregion

    //#region TestContent

    //private string _testContent = "Готово!";

    ///// <summary>Статус программы</summary>
    //public string TestContent
    //{
    //    get => _testContent;
    //    set => Set(ref _testContent, value);
    //}

    //#endregion

    #region Commands

    #region CloseApplicationCommand

    public ICommand CloseApplicationCommand { get; }

    private void OnCloseApplicationCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }

    private bool CanCloseApplicationCommandExecute(object p) => true;

    #endregion

    //#region CheckBoxStatusChangedCommand

    //public ICommand CheckBoxStatusChangedCommand { get; }

    //private void OnCheckBoxStatusChangedCommandExecuted(object p)
    //{
    //    if (this._checkStatus)
    //    {
    //        TestContent = "Checked";
    //    }
    //    else
    //    {
    //        TestContent = "Unchecked";
    //    }
    //}

    //private bool CanCheckBoxStatusChangedCommandExecute(object p) => true;

    //#endregion

    #endregion

    public MainWindowViewModel()
    {
        #region Commands

        CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

        //CheckBoxStatusChangedCommand = new LambdaCommand(OnCheckBoxStatusChangedCommandExecuted, CanCheckBoxStatusChangedCommandExecute);

        #endregion

    }

    //private string GetTestContent()
    //{
    //    return CheckStatus ? "Checked" : "Unchecked";
    //}



}
