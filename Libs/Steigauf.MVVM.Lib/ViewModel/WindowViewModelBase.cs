using System.Windows;
using System.Windows.Input;

namespace Steigauf.MVVM
{
    public abstract class WindowViewModelBase : ViewModelBase, System.IDisposable
    {
        protected bool _disposed;

        private Steigauf.MVVM.Command.DelegateCommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new Steigauf.MVVM.Command.DelegateCommand(Exit);
                }
                return exitCommand;
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        //protected abstract void onWindowLoaded();

        //private Steigauf.MVVM.Command.DelegateCommand loadedCommand;
        //public ICommand WindowLoadedCommand
        //{
        //    get
        //    {
        //        if (loadedCommand == null)
        //        {
        //            loadedCommand = new Steigauf.MVVM.Command.DelegateCommand(onWindowLoaded);
        //        }
        //        return loadedCommand;
        //    }
        //}


        #region "Closing Application"
        //muss ins MainWindow
        //xmlns:nsmvvm="clr-namespace:Steigauf.MVVM"
        //nsmvvm:WindowClosingBehavior.Closed="{Binding ClosedCommand}"
        //nsmvvm:WindowClosingBehavior.Closing="{Binding ClosingCommand}"
        //nsmvvm:WindowClosingBehavior.CancelClosing="{Binding CancelClosingCommand}"

        private Steigauf.MVVM.Command.DelegateCommand closedCommand;
        public ICommand WindowClosedCommand
        {
            get
            {
                if (closedCommand == null)
                {
                    closedCommand = new Steigauf.MVVM.Command.DelegateCommand(onWindowClosed);
                }
                return closedCommand;
            }
        }

        protected abstract void onWindowClosed();

        private Steigauf.MVVM.Command.DelegateCommand closingCommand;

        public ICommand WindowClosingCommand
        {
            get
            {
                if (closingCommand == null)
                {
                    closingCommand = new Steigauf.MVVM.Command.DelegateCommand(onWindowClosing, canWindowClose);
                }
                return closingCommand;
            }
        }

        protected abstract void onWindowClosing();
        protected abstract bool canWindowClose();

        private Steigauf.MVVM.Command.DelegateCommand cancelClosingCommand;
        public ICommand CancelWindowClosingCommand
        {
            get
            {
                if (cancelClosingCommand == null)
                {
                    cancelClosingCommand = new Steigauf.MVVM.Command.DelegateCommand(cancelWindowClosing);
                }
                return cancelClosingCommand;
            }
        }

        protected abstract void cancelWindowClosing();

        #endregion

        #region "Prism"
        /// <summary>
        /// Stellt einen Ort für den Dialogservice bereit
        /// </summary>
        public virtual Steigauf.MVVM.Service.IDialogService DialogService
        {
            get;
            set;
        }
        #endregion


        #region Closed Event
        public event System.EventHandler Closed;

        protected void OnClosed()
        {
            System.EventHandler handler = this.Closed;
            if (handler != null)
            {
                handler(this, System.EventArgs.Empty);
            }
        }
        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //_entities.Dispose();
                }

                _disposed = true;
            }
        }

        ~WindowViewModelBase()
        {
            Dispose(false);
        }

        #endregion
    }
}
