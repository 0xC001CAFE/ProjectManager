﻿namespace ProjectManager.WPF.Messaging.Messages
{
    public class ChangeStateMessage<T>
    {
        public T NewState { get; }

        public ChangeStateMessage(T newState)
        {
            NewState = newState;
        }
    }
}
