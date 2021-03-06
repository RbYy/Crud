using CrudLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrudLib.ViewModels
{
	public class ViewModelBase : ObservableObject
	{
		//protected GenericDBActions<Person> DataService = new PeopleDBActions(new PeopleDbContextFactory());

		public IPeopleDataActions<Person> DataService { get; set; }
		public ViewModelBase(IPeopleDataActions<Person> dataService)
		{
			DataService = dataService;
		}
	}
}
