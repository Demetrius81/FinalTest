using PetAccountSystem.Models.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PetAccountSystem.AppWPF.Services.Interfaces;

/// <summary>
/// Интерфейс основной логики приложения
/// </summary>
internal interface ILogic
{
    /// <summary>
    /// Получить всех питомцев
    /// </summary>
    /// <param name="cancel">Токен отмены</param>
    /// <returns>Коллекция питомцев</returns>
    Task<IEnumerable<Pet>> GetAllPetsAsync(CancellationToken cancel = default);

    /// <summary>
    /// Добавить новый вид питомца
    /// </summary>
    /// <param name="kindOfPet">Название вида питомца</param>
    /// <param name="isPack">Въючный или домашний</param>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task<bool> AddNewAnimal(string kindOfPet, bool isPack, CancellationToken cancel = default);

    /// <summary>
    /// Добавление питомцев
    /// </summary>
    /// <param name="count">Количество питомцев</param>
    /// <param name="pet">Объект питомец</param>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task<Pet> AddUpdatePetsCount(int count, Pet pet, CancellationToken cancel = default);

    /// <summary>
    /// Получение всех питомцев, кроме тех, численность которых равна 0
    /// </summary>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task<IEnumerable<Pet>> GetPetsAsync(CancellationToken cancel = default);

    /// <summary>
    /// Получение всех домашних питомцев, кроме тех, численность которых равна 0
    /// </summary>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task<IEnumerable<Pet>> GetPetsHomeAsync(CancellationToken cancel = default);

    /// <summary>
    /// Получение всех въючных питомцев, кроме тех, численность которых равна 0
    /// </summary>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task<IEnumerable<Pet>> GetPetsPackAsync(CancellationToken cancel = default);

    /// <summary>
    /// Удаление питомцев
    /// </summary>
    /// <param name="count">Количество питомцев</param>
    /// <param name="pet">Объект питомца</param>
    /// <param name="cancel">Токен отмены</param>
    /// <returns></returns>
    Task<Pet> RemoveUpdatePetsCount(int count, Pet pet, CancellationToken cancel = default);
}