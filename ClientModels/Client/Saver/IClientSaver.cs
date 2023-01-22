namespace ClientModels
{
    public interface IClientSaver
    {
        void Save<T>(string login, T handler);
        T? Read<T>(string login);
    }
}