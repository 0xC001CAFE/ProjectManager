using ProjectManager.WPF.Commands;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.Models;
using ProjectManager.WPF.ViewModels.Locator;
using ProjectManager.WPF.ViewModels.States;
using System.Windows.Input;

namespace ProjectManager.WPF.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        #region Properties for data binding

        private ProjectModel project;
        public ProjectModel Project
        {
            get => project;
            private set
            {
                project = value;

                OnPropertyChanged(nameof(Project));
            }
        }

        private ProjectTaskModel selectedTask;
        public ProjectTaskModel SelectedTask
        {
            get => selectedTask;
            set
            {
                selectedTask = value;

                OnPropertyChanged(nameof(SelectedTask));

                messenger.Send(new SelectionChangedMessage<ProjectTaskModel>(selectedTask));
            }
        }

        public ICommand NewTaskCommand { get; }

        #endregion

        public ProjectViewModel(IMessenger messenger,
                                IViewModelLocator viewModelLocator) : base(messenger, viewModelLocator)
        {
            #region Commands

            NewTaskCommand = new RelayCommand(() =>
            {
                messenger.Send(new ChangeStateMessage<EditableTaskViewModelState>(EditableTaskViewModelState.CreateNew));
            }, () => project != null);

            #endregion

            #region Messenger

            messenger.Subscribe<SelectionChangedMessage<ProjectModel>>(message =>
            {
                Project = message.SelectedElement;
            });

            messenger.Subscribe<ChangeSelectionMessage<ProjectTaskModel>>(message =>
            {
                SelectedTask = message.NewSelectedElement;
            });

            #endregion
        }
    }
}
