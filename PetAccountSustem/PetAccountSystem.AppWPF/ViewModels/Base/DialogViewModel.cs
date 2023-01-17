using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAccountSystem.AppWPF.ViewModels.Base;
internal abstract class DialogViewModel : TitledViewModel
{
    public event EventHandler? DialogComplete;

    protected virtual void OnDialogComplete(EventArgs e) => DialogComplete?.Invoke(this, e);
}
