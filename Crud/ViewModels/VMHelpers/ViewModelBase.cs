using Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crud.ViewModels
{
	public class ViewModelBase : ObservableObject
	{
		protected GenericDBActions<Person> dataActions = new PeopleDBActions(new PeopleDbContextFactory());


		public ViewModelBase()
		{

		}
	}
}
