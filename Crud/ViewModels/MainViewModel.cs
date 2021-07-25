using Crud.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Crud.ViewModels
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

		public Msg _Msg { get; set; }
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

		public MainViewModel()
		{
			CmdCreate = new ActionCommand(Create);
			CmdUpdate = new ActionCommand(Update);
			CmdDelete = new ActionCommand(Delete);
			CmdSearch = new ActionCommand(Search);
			PeopleList = new ObservableCollection<Person>(dataActions.GetAll().Result);
			InputPerson = new();
		}

		private void Delete(object obj)
		{
			if (SelectedPerson != null
				&& !dataActions.Delete(SelectedPerson.Id).Result)
			{
				Message = new Msg(SelectedPerson).DeleteFail;
				return;
			}
			Message = new Msg(SelectedPerson).Deleted;

			PeopleList.Remove(SelectedPerson);
			InputPerson = null;
		}

		private void Update(object obj)
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
			dataActions.Update(SelectedPerson);
			var index = PeopleList.IndexOf(PeopleList.First((p) => p.Id == InputPerson.Id));
			PeopleList[index] = SelectedPerson;
			Message = new Msg(InputPerson).Updated;
		}

		private void Create(object o)
		{
			InputPerson ??= new();
			if (PeopleList.Any(person => person.FiscalNumber == InputPerson.FiscalNumber))
			{
				Message = new Msg(SelectedPerson).AlreadyExists; return;
			}

			var newPerson = new Person(InputPerson);
			newPerson.Id = 0;

			PeopleList.Add(dataActions.Create(newPerson).Result);
			SelectedPerson = InputPerson = newPerson;
			Message = new Msg(SelectedPerson).Created;

		}

		// Searches a person by fiscal number (retreiving)
		private void Search(object o)
		{
			Person found = (dataActions as PeopleDBActions).GetByFiscal(InputPerson.FiscalNumber).Result;
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
			var people = await dataActions.GetAll();
			return people.Count();
		}

	}
}
