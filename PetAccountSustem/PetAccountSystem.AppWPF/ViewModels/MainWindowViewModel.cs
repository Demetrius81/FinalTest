using PetAccountSystem.AppWPF.Infrastructure.Commands;
using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class MainWindowViewModel : ViewModel
{
    #region Title

    private string _title = "Система учета питомника животных";

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

    #region Commands

    #region CloseApplicationCommand

    public ICommand CloseApplicationCommand { get; }

    private void OnCloseApplicationCommandExecuted(object p)
    {
        Application.Current.Shutdown();
    }

    private bool CanCloseApplicationCommandExecute(object p) => true;

    #endregion

    #endregion

    public MainWindowViewModel()
    {
        #region Commands

        CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

        #endregion

    }




}
