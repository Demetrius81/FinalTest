using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace PetAccountSystem.AppWPF.ViewModels.Base;
internal abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly Lazy<Dictionary<string, object?>> _values = new(() => new(), LazyThreadSafetyMode.PublicationOnly);

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected virtual T? Get<T>([CallerMemberName] string propertyName = null!)
    {
        if (propertyName is null)
            throw new ArgumentNullException(nameof(propertyName));

        if (!_values.IsValueCreated)
            return default;

        if (_values.Value is not { Count: > 0 } values)
            return default;

        return values.TryGetValue(propertyName, out var value) ? (T?)value : default;
    }

    protected virtual bool Set<T>(T value, [CallerMemberName] string propertyName = null!)
    {
        if (propertyName is null)
            throw new ArgumentNullException(nameof(propertyName));

        var values = _values.Value;
        if (values.TryGetValue(propertyName, out var oldValue) && Equals(oldValue, value))
            return false;

        values[propertyName] = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
