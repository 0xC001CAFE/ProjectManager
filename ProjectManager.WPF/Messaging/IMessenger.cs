using System;

namespace ProjectManager.WPF.Messaging
{
    public interface IMessenger
    {
        void Subscribe<T>(Action<T> callback);

        void Unsubscribe<T>(object subscriber);

        void Send<T>(T message);
    }
}
