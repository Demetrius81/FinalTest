using PetAccountSystem.AppWPF.Infrastructure.Commands.Base;
using System;

namespace PetAccountSystem.AppWPF.Infrastructure.Commands;
internal class LambdaCommand : Command
{
    private readonly Delegate? _execute;
    private readonly Delegate? _canExecute;

    public LambdaCommand(Action<object?> execute, Func<bool>? canExecute = null)
    {
        this._execute = execute;
        this._canExecute = canExecute;
    }

    public LambdaCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        this._execute = execute;
        this._canExecute = canExecute;
    }

    public LambdaCommand(Action execute, Func<object?, bool>? canExecute = null)
    {
        this._execute = execute;
        this._canExecute = canExecute;
    }

    public LambdaCommand(Action execute, Func<bool>? canExecute = null)
    {
        this._execute = execute;
        this._canExecute = canExecute;
    }

    protected override bool CanExecute(object? parameter)
    {
        if (!base.CanExecute(parameter))
        {
            return false;
        }

        return _canExecute switch
        {
            null => true,
            Func<bool> canExec => canExec(),
            Func<object?, bool> canExec => canExec(parameter),
            _ => throw new InvalidOperationException($"Type of delegate {_canExecute.GetType()} not supported"),
        };
    }

    protected override void Execute(object? parameter)
    {
        switch (_execute)
        {
            default:
                throw new InvalidOperationException($"Type of delegate {_canExecute.GetType()} not supported");
            case null:
                throw new InvalidOperationException($"Command invocation delegate not specified");
            case Action execute:
                execute();
                break;
            case Action<object?> execute:
                execute(parameter);
                break;
        }
    }
}
