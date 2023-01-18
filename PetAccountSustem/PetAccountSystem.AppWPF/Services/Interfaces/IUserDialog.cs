namespace PetAccountSystem.AppWPF.Services.Interfaces;
internal interface IUserDialog
{
    /// <summary>Метод открытия главного окна</summary>
    void OpenMainWindow();

    /// <summary>Метод открытия окна добавления питомцев</summary>
    void OpenAddWindow();

    /// <summary>Метод открытия окна удаления питомцев</summary>
    void OpenRemoveWindow();

    /// <summary>Метод открытия окна добавления нового вида питомца</summary>
    void OpenAddKindOfPetsWindow();

    /// <summary>Метод открытия окна ошибки</summary>
    void OpenRemoveErrorWindow();

    /// <summary>Метод открытия окна ошибки</summary>
    void OpenAddErrorWindow();

    /// <summary>Метод открытия окна ошибки</summary>
    void OpenAddKindErrorWindow();
}
