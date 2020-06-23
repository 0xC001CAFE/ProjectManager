namespace ProjectManager.WPF.Messaging.Messages
{
    public class ChangeSelectionMessage<T>
    {
        public T NewSelectedElement { get; }

        public ChangeSelectionMessage(T newSelectedElement)
        {
            NewSelectedElement = newSelectedElement;
        }
    }
}
