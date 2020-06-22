using ProjectManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.WPF.Models
{
    public abstract class ProjectComponentModel : ObservableObject
    {
        public string Id { get; private set; }

        #region Properties for data binding

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

        protected void Map(in ProjectComponent projectComponent)
        {
            Id = projectComponent.Id;
            StartDate = projectComponent.DateRange.StartDate;
            EndDate = projectComponent.DateRange.EndDate;
            Name = projectComponent.Name;
            Description = projectComponent.Description;
        }

        protected void MapBack(ProjectComponent projectComponent)
        {
            projectComponent.DateRange = new DateRange
            {
                StartDate = startDate ?? default,
                EndDate = endDate ?? default
            };
            projectComponent.Name = name;
            projectComponent.Description = description;
        }
    }
}
