using Microsoft.Extensions.DependencyInjection;
using PetAccountSystem.AppWPF.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAccountSystem.AppWPF.Services;
internal class UserDialogService : IUserDialog
{
    private readonly IServiceProvider _services;

    private MainWindow? _mainWindow;
    private AddWindow? _addWindow;
    private RemoveWindow? _removeWindow;
    private AddKindOfPetsWindow? _addKindOfPetsWindow;

    public UserDialogService(IServiceProvider services)
    {
        this._services = services;
    }

    public void OpenMainWindow()
    {
        if (this._mainWindow is { } window)
        {
            window.Show();
            return;
        }

        window = this._services.GetRequiredService<MainWindow>();
        window.Closed += (_, _) => this._mainWindow = null;
        this._mainWindow = window;
        window.Show();
    }

    public void OpenAddWindow()
    {
        if (this._addWindow is { } window)
        {
            window.Show();
            return;
        }

        window = this._services.GetRequiredService<AddWindow>();
        window.Closed += (_, _) => this._addWindow = null;
        this._addWindow = window;
        window.Show();
    }

    public void OpenRemoveWindow()
    {
        if (this._removeWindow is { } window)
        {
            window.Show();
            return;
        }

        window = this._services.GetRequiredService<RemoveWindow>();
        window.Closed += (_, _) => this._removeWindow = null;
        this._removeWindow = window;
        window.Show();
    }

    public void OpenAddKindOfPetsWindow()
    {
        if (this._addKindOfPetsWindow is { } window)
        {
            window.Show();
            return;
        }

        window = this._services.GetRequiredService<AddKindOfPetsWindow>();
        window.Closed += (_, _) => this._addKindOfPetsWindow = null;
        this._addKindOfPetsWindow = window;
        window.Show();
    }
}
