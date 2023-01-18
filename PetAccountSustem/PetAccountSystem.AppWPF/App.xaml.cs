using Microsoft.Extensions.DependencyInjection;
using PetAccountSystem.AppWPF.Infrastructure.Extensions;
using PetAccountSystem.AppWPF.Services.Interfaces;
using System;
using System.Windows;

namespace PetAccountSystem.AppWPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private static IServiceProvider? _services;

    public static IServiceProvider? Services => _services ??= InitialzeServices()?.BuildServiceProvider();

    private static IServiceCollection? InitialzeServices()
    {
        var services = new ServiceCollection();
        services
            .ServicesSubscription()
            .WindowsSubscription();
        return services;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        Services?.GetRequiredService<IUserDialog>().OpenMainWindow();
    }
}
