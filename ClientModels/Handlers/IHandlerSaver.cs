namespace ClientModels
{
    public interface IHandlerSaver<T> where T: IHandler
    {
        void Save(T handler);
        T? Read();
    }
}