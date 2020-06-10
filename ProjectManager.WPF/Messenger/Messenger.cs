using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProjectManager.WPF.Messenger
{
    public class Messenger : IMessenger
    {
        private readonly Dictionary<Type, List<MessageRegistration>> registrations = new Dictionary<Type, List<MessageRegistration>>();

        public void Register<T>(Action<T> callback)
        {
            if (!registrations.ContainsKey(typeof(T))) registrations.Add(typeof(T), new List<MessageRegistration>());

            registrations[typeof(T)].Add(new MessageRegistration(callback.Target, typeof(T), callback));
        }

        public void Send<T>(T message)
        {
            GetCallbacks(typeof(T))?.ForEach(callback => callback.DynamicInvoke(message));
        }

        public void Unregister<T>(object receiver)
        {
            if (!registrations.ContainsKey(typeof(T))) return;

            var messageRegistrations = registrations[typeof(T)];
            messageRegistrations.RemoveAll(messageRegistration => messageRegistration.Receiver == receiver);

            if (messageRegistrations.Count == 0) registrations.Remove(typeof(T));
        }

        private List<Delegate> GetCallbacks(Type messageType)
        {
            if (!registrations.ContainsKey(messageType)) return null;

            var callbacks = new List<Delegate>();

            var messageRegistrations = registrations[messageType];
            messageRegistrations.RemoveAll(messageRegistration =>
            {
                if (!messageRegistration.IsAlive) return true;

                callbacks.Add(messageRegistration.Callback);

                return false;
            });

            if (messageRegistrations.Count == 0) registrations.Remove(messageType);

            return callbacks;
        }

        private class MessageRegistration
        {
            private readonly WeakReference receiver;
            private readonly Type callbackType;
            private readonly MethodInfo callbackMethod;

            public Delegate Callback
            {
                get
                {
                    if (receiver == null) return Delegate.CreateDelegate(callbackType, callbackMethod);

                    var target = receiver.Target;
                    if (target != null) return Delegate.CreateDelegate(callbackType, target, callbackMethod);

                    return null;
                }
            }

            public bool IsAlive => receiver.IsAlive;

            public object Receiver => receiver?.Target;

            public MessageRegistration(object receiver, Type messageType, Delegate callback)
            {
                this.receiver = receiver == null ? null : new WeakReference(receiver);
                callbackType = typeof(Action<>).MakeGenericType(messageType);
                callbackMethod = callback.Method;
            }
        }
    }
}
