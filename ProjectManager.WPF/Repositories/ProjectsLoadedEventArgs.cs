using System;

namespace ProjectManager.WPF.Repositories
{
    public class ProjectsLoadedEventArgs : EventArgs
    {
        public bool LoadedSuccessfully { get; }

        public Exception Exception { get; }

        public ProjectsLoadedEventArgs(Exception exception)
        {
            LoadedSuccessfully = exception == null;
            Exception = exception;
        }
    }
}
