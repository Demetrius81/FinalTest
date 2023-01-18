namespace PetAccountSystem.AppWPF.Services.Interfaces;
internal interface IUserDialog
{
    void OpenMainWindow();

    void OpenAddWindow();

    void OpenRemoveWindow();

    void OpenAddKindOfPetsWindow();

    void OpenRemoveErrorWindow();

    void OpenAddErrorWindow();

    void OpenAddKindErrorWindow();
}
