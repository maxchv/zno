namespace Zno.DAL.Entities
{
    /// <summary>
    /// Статус пользователя
    /// </summary>
    public enum Status
    {
        // В сети
        Online,
        // Не в сети
        NotOnline,
        // Проходит тест
        PassesTest
    }
}