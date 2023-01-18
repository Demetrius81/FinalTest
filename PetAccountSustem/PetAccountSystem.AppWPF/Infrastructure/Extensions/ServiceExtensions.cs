using Microsoft.Extensions.DependencyInjection;
using PetAccountSystem.AppWPF.Services.Implementation;
using PetAccountSystem.AppWPF.Services.Interfaces;
using PetAccountSystem.AppWPF.ViewModels;
using PetAccountSystem.AppWPF.Views.Windows;
using PetAccountSystem.Client.Pets;
using PetAccountSystem.Interfaces.Repositories;
using PetAccountSystem.Models.Models;
using System;
using System.Net.Http;

namespace PetAccountSystem.AppWPF.Infrastructure.Extensions;
internal static class ServiceExtensions
{
    private const string BASE_ADDRESS = "http://localhost:5241";

    public static IServiceCollection ServicesSubscription(this IServiceCollection services)
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri(BASE_ADDRESS)
        };

        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<AddWindowViewModel>();
        services.AddTransient<RemoveWindowViewModel>();
        services.AddTransient<AddKindOfPetsWindowViewModel>();
        services.AddTransient<AddErrorWindowViewModel>();
        services.AddTransient<RemoveErrorWindowViewModel>();
        services.AddTransient<AddKindErrorWindowViewModel>();
        services.AddTransient<ILogic, DomainLogic>();
        services.AddTransient<IRepositoryAsync<Pet>, PetsClient>(x => new PetsClient(client));
        services.AddTransient<IUserDialog, UserDialogService>();

        return services;
    }

    public static IServiceCollection WindowsSubscription(this IServiceCollection services)
    {
        services.AddTransient(s =>
        {
            var model = s.GetRequiredService<MainWindowViewModel>();
            var window = new MainWindow { DataContext = model };
            model.DialogComplete += (_, _) => window.Close();
            return window;
        });
        services.AddTransient(s =>
        {
            var model = s.GetRequiredService<AddWindowViewModel>();
            var window = new AddWindow { DataContext = model };
            model.DialogComplete += (_, _) => window.Close();
            return window;
        });
        services.AddTransient(s =>
        {
            var model = s.GetRequiredService<RemoveWindowViewModel>();
            var window = new RemoveWindow { DataContext = model };
            model.DialogComplete += (_, _) => window.Close();
            return window;
        });
        services.AddTransient(s =>
        {
            var model = s.GetRequiredService<AddKindOfPetsWindowViewModel>();
            var window = new AddKindOfPetsWindow { DataContext = model };
            model.DialogComplete += (_, _) => window.Close();
            return window;
        });
        services.AddTransient(s =>
        {
            var model = s.GetRequiredService<AddErrorWindowViewModel>();
            var window = new AddErrorWindow { DataContext = model };
            model.DialogComplete += (_, _) => window.Close();
            return window;
        });
        services.AddTransient(s =>
        {
            var model = s.GetRequiredService<RemoveErrorWindowViewModel>();
            var window = new RemoveErrorWindow { DataContext = model };
            model.DialogComplete += (_, _) => window.Close();
            return window;
        });
        services.AddTransient(s =>
        {
            var model = s.GetRequiredService<AddKindErrorWindowViewModel>();
            var window = new AddKindErrorWindow { DataContext = model };
            model.DialogComplete += (_, _) => window.Close();
            return window;
        });

        return services;
    }
}
