using System;

namespace ClientModels
{
    public interface ISyncHandler<T>: IDisposable
    {
        T Sync(string login, T notifications, string uri);
    }
}