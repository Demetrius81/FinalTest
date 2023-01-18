using System;

namespace PetAccountSystem.AppWPF.ViewModels.Base;
internal abstract class DialogViewModel : TitledViewModel
{
    public event EventHandler? DialogComplete;

    protected virtual void OnDialogComplete(EventArgs e) => DialogComplete?.Invoke(this, e);
}
