using Microsoft.Extensions.DependencyInjection;
using PetAccountSystem.AppWPF.Services.Interfaces;
using PetAccountSystem.AppWPF.Views.Windows;
using System;

namespace PetAccountSystem.AppWPF.Services.Implementation;
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
        _services = services;
    }

    public void OpenMainWindow()
    {
        if (_mainWindow is { } window)
        {
            window.Show();
            return;
        }

        window = _services.GetRequiredService<MainWindow>();
        window.Closed += (_, _) => _mainWindow = null;
        _mainWindow = window;
        window.Show();
    }

    public void OpenAddWindow()
    {
        if (_addWindow is { } window)
        {
            window.Show();
            return;
        }

        window = _services.GetRequiredService<AddWindow>();
        window.Closed += (_, _) => _addWindow = null;
        _addWindow = window;
        window.Show();
    }

    public void OpenRemoveWindow()
    {
        if (_removeWindow is { } window)
        {
            window.Show();
            return;
        }

        window = _services.GetRequiredService<RemoveWindow>();
        window.Closed += (_, _) => _removeWindow = null;
        _removeWindow = window;
        window.Show();
    }

    public void OpenAddKindOfPetsWindow()
    {
        if (_addKindOfPetsWindow is { } window)
        {
            window.Show();
            return;
        }

        window = _services.GetRequiredService<AddKindOfPetsWindow>();
        window.Closed += (_, _) => _addKindOfPetsWindow = null;
        _addKindOfPetsWindow = window;
        window.Show();
    }

    public void OpenAddKindErrorWindow()
    {
        if (_addKindErrorWindow is { } window)
        {
            window.Show();
            return;
        }

        window = _services.GetRequiredService<AddKindErrorWindow>();
        window.Closed += (_, _) => _addKindErrorWindow = null;
        _addKindErrorWindow = window;
        window.Show();
    }

    public void OpenAddErrorWindow()
    {
        if (_addErrorWindow is { } window)
        {
            window.Show();
            return;
        }

        window = _services.GetRequiredService<AddErrorWindow>();
        window.Closed += (_, _) => _addErrorWindow = null;
        _addErrorWindow = window;
        window.Show();
    }

    public void OpenRemoveErrorWindow()
    {
        if (_removeErrorWindow is { } window)
        {
            window.Show();
            return;
        }

        window = _services.GetRequiredService<RemoveErrorWindow>();
        window.Closed += (_, _) => _removeErrorWindow = null;
        _removeErrorWindow = window;
        window.Show();
    }
}
