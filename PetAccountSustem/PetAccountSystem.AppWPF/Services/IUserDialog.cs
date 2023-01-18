namespace PetAccountSystem.AppWPF.Services;
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
