using System;
using System.Diagnostics;
using System.Windows.Input;

namespace mynamespace
{

    class Command<T> : ICommand
    {
        private readonly Action<T> _execute = null;
        private readonly Predicate<object> _canExecute = null;        
        public Command(Action<T> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add{CommandManager.RequerySuggested += value;}
            remove{CommandManager.RequerySuggested -= value;}
        }
        public void Execute(object parameter)
        {
            if (parameter is T)
            {
                var typedParameter = (T)parameter;
                _execute(typedParameter);
            }
        }
    }
}
