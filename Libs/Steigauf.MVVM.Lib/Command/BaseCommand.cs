namespace Steigauf.MVVM.Command
{
    public class BaseCommand : GenericCommand<object>
    {
        public BaseCommand(Action<object> command) : base(command, null)
        {
        }

        public BaseCommand(Action<object> command, Func<object, bool> canExecute) : base(command, canExecute)
        {
        }

        /*
                 private GenericCommand<object> _command;

        public BaseCommand(Action<object> command)
        {
            _command = new GenericCommand<object>(command);
        }

        public BaseCommand(Action<object> command, Func<object, bool> canExecute)
        {
            _command = new GenericCommand<object>(command, canExecute);
        }

        public bool CanExecute(object parameter)
        {
            return _command.CanExecute(parameter);
        }


        
        public event EventHandler CanExecuteChanged
        {
          add
          {
              _command.CanExecuteChanged += value;
              CommandManager.RequerySuggested += value;
          }
          remove
          {
              _command.CanExecuteChanged -= value;
              CommandManager.RequerySuggested -= value;
          }
        }


        public void Execute(object parameter)
        {
            _command.Execute(parameter);
        }
         */

    }
}
