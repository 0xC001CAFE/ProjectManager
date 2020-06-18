using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.WPF.Commands
{
    public class AsyncCommand : ICommand
    {
        private bool isBusy;
        private readonly Func<Task> execute;
        private readonly Func<bool> canExecute;
        private readonly Action<bool> onCompletion;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public AsyncCommand(Func<Task> execute,
                            Action<bool> onCompletion) : this(execute, null, onCompletion) { }

        public AsyncCommand(Func<Task> execute,
                            Func<bool> canExecute = null,
                            Action<bool> onCompletion = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
            this.onCompletion = onCompletion;
        }

        public bool CanExecute(object parameter)
        {
            return !isBusy && (canExecute == null || canExecute());
        }

        public async void Execute(object parameter)
        {
            var success = true;

            try
            {
                isBusy = true;

                await execute();
            }
            catch
            {
                success = false;
            }
            finally
            {
                isBusy = false;

                CommandManager.InvalidateRequerySuggested();

                onCompletion?.Invoke(success);
            }
        }
    }
}
