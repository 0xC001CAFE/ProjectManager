using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProjectManager.WPF.Messaging
{
    public class Messenger : IMessenger
    {
        private readonly Dictionary<Type, List<Subscription>> messengerSubscriptions = new Dictionary<Type, List<Subscription>>();

        public void Send<T>(T message)
        {
            var messageType = typeof(T);

            if (!messengerSubscriptions.ContainsKey(messageType)) return;

            var subscriptions = messengerSubscriptions[messageType];
            subscriptions.RemoveAll(subscription => subscription.CanBeRemoved);

            if (TryRemoveMessageEntry(messageType)) return;

            subscriptions.ForEach(subscription => subscription.InvokeCallback(message));
        }

        public void Subscribe<T>(Action<T> callback)
        {
            var messageType = typeof(T);
            var subscription = new Subscription(callback.Target, callback.Method);

            if (!messengerSubscriptions.ContainsKey(messageType))
            {
                messengerSubscriptions.Add(messageType, new List<Subscription> { subscription });

                return;
            }

            messengerSubscriptions[messageType].Add(subscription);
        }

        public void Unsubscribe<T>(object subscriber)
        {
            var messageType = typeof(T);

            if (!messengerSubscriptions.ContainsKey(messageType)) return;

            var subscriptions = messengerSubscriptions[messageType];
            subscriptions.RemoveAll(subscription => subscription.CanBeRemoved || subscription.Subscriber == subscriber);

            TryRemoveMessageEntry(messageType);
        }

        private bool TryRemoveMessageEntry(Type messageType)
        {
            if (messengerSubscriptions[messageType].Count == 0)
            {
                messengerSubscriptions.Remove(messageType);

                return true;
            }

            return false;
        }

        private class Subscription
        {
            private readonly WeakReference subscriber;
            private readonly MethodInfo callback;

            public bool CanBeRemoved => !subscriber.IsAlive;

            public object Subscriber => subscriber.Target;

            public Subscription(object subscriber, MethodInfo callback)
            {
                this.subscriber = new WeakReference(subscriber);
                this.callback = callback;
            }

            public void InvokeCallback(object message)
            {
                if (callback.IsStatic)
                {
                    callback.Invoke(null, new object[] { message });

                    return;
                }

                // check if subscriber object has been garbage collected
                if (Subscriber == null) return;

                callback.Invoke(Subscriber, new object[] { message });
            }
        }
    }
}
