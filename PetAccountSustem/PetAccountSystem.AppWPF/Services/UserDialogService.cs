using Microsoft.Extensions.DependencyInjection;
using PetAccountSystem.AppWPF.Views.Windows;
using System;

namespace PetAccountSystem.AppWPF.Services;
internal class UserDialogService : IUserDialog
{
    private readonly IServiceProvider _services;

    private MainWindow? _mainWindow;
    private AddWindow? _addWindow;
    private RemoveWindow? _removeWindow;
    private AddKindOfPetsWindow? _addKindOfPetsWindow;
    private AddKindErrorWindow? _addKindErrorWindow;
    private AddErrorWindow? _addErrorWindow;
    private RemoveErrorWindow? _removeErrorWindow;

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

    public void OpenAddKindErrorWindow()
    {
        if (this._addKindErrorWindow is { } window)
        {
            window.Show();
            return;
        }

        window = this._services.GetRequiredService<AddKindErrorWindow>();
        window.Closed += (_, _) => this._addKindErrorWindow = null;
        this._addKindErrorWindow = window;
        window.Show();
    }

    public void OpenAddErrorWindow()
    {
        if (this._addErrorWindow is { } window)
        {
            window.Show();
            return;
        }

        window = this._services.GetRequiredService<AddErrorWindow>();
        window.Closed += (_, _) => this._addErrorWindow = null;
        this._addErrorWindow = window;
        window.Show();
    }

    public void OpenRemoveErrorWindow()
    {
        if (this._removeErrorWindow is { } window)
        {
            window.Show();
            return;
        }

        window = this._services.GetRequiredService<RemoveErrorWindow>();
        window.Closed += (_, _) => this._removeErrorWindow = null;
        this._removeErrorWindow = window;
        window.Show();
    }
}
