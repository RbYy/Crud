using CrudLib.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrudLib.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		public ICommand CmdDelete { get; set; }
		public ICommand CmdUpdate { get; set; }
		public ICommand CmdCreate { get; set; }
		public ICommand CmdSearch { get; set; }

		private Person selectedPerson;
		private string message;
		private ObservableCollection<Person> peopleList;
		private Person inputPerson;

		public ObservableCollection<Person> PeopleList {	get => peopleList; set { peopleList = value; OnPropertyChanged(nameof(PeopleList)); } }
		public Person InputPerson	{ get => inputPerson;	set { inputPerson	= value; OnPropertyChanged(nameof(InputPerson)); } }
		public string Message		{ get => message;		set { message		= value; OnPropertyChanged(nameof(Message)); } }
		public Person SelectedPerson
		{
			get => selectedPerson;
			set
			{
				selectedPerson = value;
				if (selectedPerson == null) return;
				OnPropertyChanged(nameof(SelectedPerson));
				InputPerson = new(selectedPerson);
			}
		}

		public MainViewModel(IPeopleDataActions<Person> dataService) : base(dataService)
		{
			CmdCreate = new ActionCommand(Create);
			CmdUpdate = new ActionCommand(Update);
			CmdDelete = new ActionCommand(Delete);
			CmdSearch = new ActionCommand(GetByFiscal);
			PeopleList = new ObservableCollection<Person>(DataService.GetAll().Result);
			InputPerson = new();
		}

		public void Delete(object obj)
		{
			// A person must be selected from the list in order to be possibly removed
			// Prevents attempts to remove inexisting people
			if (SelectedPerson != null
				&& DataService.Delete(SelectedPerson.Id).Result)
			{
				Message = new Msg(SelectedPerson).Deleted;

				_ = PeopleList.Remove(SelectedPerson);
				InputPerson = new();
				return;
			}
			Message = new Msg(SelectedPerson).DeleteFail;
		}

		public void Update(object obj)
		{
			if (InputPerson == null)
			{
				Message = new Msg(SelectedPerson).SelectionRequired;
				return;
			}
			if (PeopleList.Any(person => person.FiscalNumber == InputPerson.FiscalNumber && person.Id != InputPerson.Id))
			{
				Message = new Msg(SelectedPerson).AlreadyExists;
				return;
			}
			SelectedPerson = new(InputPerson);
			DataService.Update(SelectedPerson.Id, SelectedPerson);
			var index = PeopleList.IndexOf(PeopleList.FirstOrDefault((p) => p.Id == InputPerson.Id));
			if (index < 0) return;
			PeopleList[index] = SelectedPerson;
			Message = new Msg(InputPerson).Updated;
		}

		public void Create(object o)
		{
			InputPerson ??= new();
			if (PeopleList.Any(person => person.FiscalNumber == InputPerson.FiscalNumber))
			{
				Message = new Msg(SelectedPerson).AlreadyExists;
				return;
			}

			var newPerson = new Person(InputPerson);
			newPerson.Id = 0;

			PeopleList.Add(DataService.Create(newPerson).Result);
			SelectedPerson = InputPerson = newPerson;
			Message = new Msg(SelectedPerson).Created;

		}

		// Searches a person by fiscal number (retreiving)
		public void GetByFiscal(object o)
		{
			Person found = DataService.GetByFiscal(InputPerson.FiscalNumber).Result;
			if (found == null)
			{
				Message = new Msg(inputPerson).NotExist;
				InputPerson = new() { FiscalNumber = inputPerson.FiscalNumber };
				return;
			}
			InputPerson = SelectedPerson = found;
			Message = new Msg(inputPerson).Found;
		}

		public async Task<int> Count()
		{
			var people = await DataService.GetAll();
			return people.Count();
		}
	}
}
