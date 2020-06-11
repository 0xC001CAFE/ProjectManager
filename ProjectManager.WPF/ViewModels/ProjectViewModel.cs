using ProjectManager.Domain.Models;
using ProjectManager.WPF.Messaging;
using ProjectManager.WPF.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.ViewModels
{
	public class ProjectViewModel : ViewModelBase
	{
		private Project project;

		public Project Project
		{
			get => project;
			private set
			{
				project = value;

				OnPropertyChanged(nameof(Project));
			}
		}

		public ProjectViewModel(IMessenger messenger) : base(messenger)
		{
			messenger.Subscribe<PropertyChangedMessage<Project>>(message => Project = message.PropertyValue);
		}
	}
}
