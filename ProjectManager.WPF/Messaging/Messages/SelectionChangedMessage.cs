namespace ProjectManager.WPF.Messaging.Messages
{
    public class SelectionChangedMessage<T>
    {
        public T SelectedElement { get; }

        public SelectionChangedMessage(T selectedElement)
        {
            SelectedElement = selectedElement;
        }
    }
}
