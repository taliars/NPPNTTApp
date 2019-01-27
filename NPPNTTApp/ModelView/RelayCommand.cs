using System;
using System.Windows.Input;

namespace NPPNTTApp.ModelView
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> action)
        {
            ExecuteDelegate = action;
        }

        public RelayCommand(Action<object> execute,
                  Predicate<object> canExecute)
        {
            ExecuteDelegate = execute;
            CanExecuteDelegate = canExecute;
        }

        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
            {
                return CanExecuteDelegate(parameter);
            }
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter)
        {
            ExecuteDelegate?.Invoke(parameter);
        }
    }
}

