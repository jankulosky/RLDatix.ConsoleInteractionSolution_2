namespace AllocateQ3_ConsoleInteraction.Interfaces
{
    public interface ILocalizer
    {
        string GetString(string key, params object[] args);
    }
}
