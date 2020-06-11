using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.Messaging.Messages
{
	public class PropertyChangedMessage<T>
	{
		public T PropertyValue { get; private set; }

		public PropertyChangedMessage(T propertyValue)
		{
			PropertyValue = propertyValue;
		}
	}
}
