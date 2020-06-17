using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.WPF.Models
{
    /// <summary>
    /// Watches a task and raises property-changed notifications when the task completes.
    /// </summary>
    public class NotifyTaskCompletion : ObservableObject
    {
        private readonly Task task;

        public bool IsCanceled => task.IsCanceled;

        public bool IsCompleted => task.IsCompleted;

        public bool IsCompletedSuccessfully => task.IsCompletedSuccessfully;

        public bool IsFaulted => task.IsFaulted;

        public NotifyTaskCompletion(Task task)
        {
            this.task = task;

            MonitorTask();
        }

        private async void MonitorTask()
        {
            try
            {
                await task;
            }
            catch { }
            finally
            {
                NotifyProperties();
            }
        }

        private void NotifyProperties()
        {
            OnPropertyChanged(nameof(IsCompleted));

            if (IsCanceled)
            {
                OnPropertyChanged(nameof(IsCanceled));

                return;
            }

            if (IsFaulted)
            {
                OnPropertyChanged(nameof(IsFaulted));

                return;
            }

            OnPropertyChanged(nameof(IsCompletedSuccessfully));
        }
    }

    /// <summary>
    /// Watches a task and raises property-changed notifications when the task completes.
    /// </summary>
    /// <typeparam name="T">The type of the task result.</typeparam>
    public class NotifyTaskCompletion<T> : ObservableObject
    {
        private readonly Task<T> task;

        public bool IsCanceled => task.IsCanceled;

        public bool IsCompleted => task.IsCompleted;

        public bool IsCompletedSuccessfully => task.IsCompletedSuccessfully;

        public bool IsFaulted => task.IsFaulted;

        public T Result => IsCompletedSuccessfully ? task.Result : default;

        public NotifyTaskCompletion(Task<T> task)
        {
            this.task = task;

            MonitorTask();
        }

        private async void MonitorTask()
        {
            try
            {
                await task;
            }
            catch { }
            finally
            {
                NotifyProperties();
            }
        }

        private void NotifyProperties()
        {
            OnPropertyChanged(nameof(IsCompleted));

            if (IsCanceled)
            {
                OnPropertyChanged(nameof(IsCanceled));

                return;
            }

            if (IsFaulted)
            {
                OnPropertyChanged(nameof(IsFaulted));

                return;
            }

            OnPropertyChanged(nameof(IsCompletedSuccessfully));
            OnPropertyChanged(nameof(Result));
        }
    }
}
