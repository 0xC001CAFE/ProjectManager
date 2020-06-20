using System;
using System.Collections.Generic;
using System.Text;

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
