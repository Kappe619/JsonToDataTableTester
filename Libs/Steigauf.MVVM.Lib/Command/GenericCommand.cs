using System.Windows.Input;

namespace Steigauf.MVVM.Command
{
    public class GenericCommand<TParameter> : ICommand where TParameter : class
    {
        public static readonly Func<TParameter, bool> DefaultCanExecuteHandler = p => p != null;

        private Action<TParameter> _command;
        private Func<TParameter, bool> _canExecuteSelector;
        private bool _enabled;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                _enabled = value;
                OnCanExecuteChange();
            }
        }

        protected virtual Action<TParameter> Command
        {
            get
            {
                return _command;
            }

            set
            {
                _command = value;
                OnCanExecuteChange();
            }
        }

        protected virtual Func<TParameter, bool> CanExecuteSelector
        {
            get
            {
                return _canExecuteSelector;
            }

            set
            {
                _canExecuteSelector = value;
                OnCanExecuteChange();
            }
        }

        public GenericCommand(Action<TParameter> command) : this(command, DefaultCanExecuteHandler)
        {
        }

        public GenericCommand(Action<TParameter> command, Func<TParameter, bool> canExecute)
        {
            _canExecuteSelector = canExecute;
            _command = command;
            _enabled = command != null;
        }

        public void Execute(object parameter)
        {
            Command((TParameter)parameter);
        }

        public virtual bool CanExecute(object parameter)
        {
            var handlerExists = Enabled && Command != null && (parameter == null || parameter is TParameter);

            if (handlerExists && CanExecuteSelector != null)
                handlerExists = CanExecuteSelector((TParameter)parameter);

            return handlerExists;
        }

        public event EventHandler CanExecuteChanged;



        protected virtual void OnCanExecuteChange()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        /*
        public event EventHandler CanExecuteChanged
        {
            add
            {
                //_command.CanExecuteChanged += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                //_command.CanExecuteChanged -= value;
                CommandManager.RequerySuggested -= value;
            }
        }*/
    }
}
