using PetAccountSystem.AppWPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAccountSystem.AppWPF.ViewModels;
internal class MainWindowViewModel : ViewModel
{
    private string _title = "Система учета питомника животных";

    /// <summary>Заголовок окна</summary>
    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }
}
