using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.ViewModels.Locator;
using ProjectManager.WPF.ViewModels.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
    public class EditableTaskViewModel : ViewModelBase
    {
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

        #endregion

        public EditableTaskViewModel(IMessenger messenger,
                                     IViewModelLocator viewModelLocator) : base(messenger, viewModelLocator)
        {

        }
    }
}
