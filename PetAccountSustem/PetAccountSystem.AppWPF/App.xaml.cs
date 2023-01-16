using Microsoft.Extensions.DependencyInjection;
using PetAccountSystem.AppWPF.Services;
using PetAccountSystem.AppWPF.ViewModels;
using PetAccountSystem.Client.Pets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace PetAccountSystem.AppWPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App// : Application
{
    private static IServiceProvider? _services;

    public static IServiceProvider? Services => _services ??= InitialzeServices()?.BuildServiceProvider();

    private static IServiceCollection? InitialzeServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<AddWindowViewModel>();
        services.AddTransient<RemoveWindowViewModel>();
        services.AddTransient<DomainLogic>();

        return services;
    }
}
