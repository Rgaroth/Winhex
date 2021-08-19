namespace Winhex.Interfaces
{
    /// <summary>
    /// Создатель логов клавиш пользователя
    /// </summary>
    public interface ILogCreator
    {
        void AddKey(string appTitle, char key);
        void Close();
    }
}