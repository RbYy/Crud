using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrudLib
{
	class ActionCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		Action<object> execute;

		public ActionCommand(Action<object> _execute)
		{
			execute = _execute;
		}

		public bool CanExecute(object parameter) => true;
		public void Execute(object parameter)
		{
			execute.Invoke(parameter);
		}
	}
}
