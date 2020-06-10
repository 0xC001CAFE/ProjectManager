using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.Messenger
{
    public interface IMessenger
    {
        void Register<T>(Action<T> callback);

        void Unregister<T>(object receiver);

        void Send<T>(T message);
    }
}
