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

    public class AsyncCommand<T> : ICommand
    {
        private bool isBusy;
        private readonly Func<Task<T>> execute;
        private readonly Func<bool> canExecute;
        private readonly Action<bool, T> onCompletion;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public AsyncCommand(Func<Task<T>> execute,
                            Action<bool, T> onCompletion) : this(execute, null, onCompletion) { }

        public AsyncCommand(Func<Task<T>> execute,
                            Func<bool> canExecute,
                            Action<bool, T> onCompletion)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
            this.onCompletion = onCompletion ?? throw new ArgumentNullException(nameof(onCompletion));
        }

        public bool CanExecute(object parameter)
        {
            return !isBusy && (canExecute == null || canExecute());
        }

        public async void Execute(object parameter)
        {
            var success = true;
            T result = default;

            try
            {
                isBusy = true;

                result = await execute();
            }
            catch
            {
                success = false;
            }
            finally
            {
                isBusy = false;

                CommandManager.InvalidateRequerySuggested();

                onCompletion(success, result);
            }
        }
    }
}
