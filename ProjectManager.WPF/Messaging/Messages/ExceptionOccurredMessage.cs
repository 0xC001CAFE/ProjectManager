using System;

namespace ProjectManager.WPF.Messaging.Messages
{
    public class ExceptionOccurredMessage
    {
        public Exception Exception { get; }

        public ExceptionOccurredMessage(Exception exception)
        {
            Exception = exception;
        }
    }
}
