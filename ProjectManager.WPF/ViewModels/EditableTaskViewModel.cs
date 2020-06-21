using ProjectManager.Domain.Models;
using ProjectManager.WPF.Commands;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using ProjectManager.WPF.ViewModels.Locator;
using ProjectManager.WPF.ViewModels.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.WPF.ViewModels
{
    public class EditableTaskViewModel : ViewModelBase
    {
        private ProjectTask editableTask;

        #region Properties for data binding

        private EditableTaskViewModelState currentState;
        public EditableTaskViewModelState CurrentState
        {
            get => currentState;
            private set
            {
                currentState = value;

                OnPropertyChanged(nameof(CurrentState));
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;

                OnPropertyChanged(nameof(Name));
            }
        }

        private DateTime? startDate;
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;

                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get => endDate;
            set
            {
                endDate = value;

                OnPropertyChanged(nameof(EndDate));
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;

                OnPropertyChanged(nameof(Description));
            }
        }

        public ICommand SaveCommand { get; }

        public ICommand EditCancelCommand { get; }

        #endregion

        public EditableTaskViewModel(IMessenger messenger,
                                     IViewModelLocator viewModelLocator) : base(messenger, viewModelLocator)
        {
            #region Commands

            SaveCommand = new AsyncCommand(Save, () =>
            {
                return currentState != EditableTaskViewModelState.DisplayOnly;
            });

            EditCancelCommand = new RelayCommand(() =>
            {
                switch (currentState)
                {
                    case EditableTaskViewModelState.DisplayOnly:
                        CurrentState = EditableTaskViewModelState.Edit;

                        break;
                    case EditableTaskViewModelState.Edit:
                    case EditableTaskViewModelState.CreateNew:
                        CurrentState = EditableTaskViewModelState.DisplayOnly;

                        Load(editableTask);

                        break;
                }
            }, () => editableTask != null);

            #endregion

            #region Messenger

            messenger.Subscribe<SelectionChangedMessage<ProjectTask>>(message =>
            {
                Load(message.SelectedElement);
            });

            #endregion
        }

        private void Load(ProjectTask projectTask = null)
        {
            editableTask = projectTask;

            CurrentState = EditableTaskViewModelState.DisplayOnly;

            Name = projectTask?.Name;
            StartDate = projectTask?.DateRange.StartDate;
            EndDate = projectTask?.DateRange.EndDate;
            Description = projectTask?.Description;
        }

        private async Task Save()
        {
            // save project task
        }
    }
}
