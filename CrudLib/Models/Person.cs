using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudLib.Models
{
	public class Person : BaseModel
	{
		private string firstName;
		private string lastName;
		private int fiscalNumber;
		private string address;


		public string	FirstName	{ get => firstName;		set { firstName		= value; OnPropertyChanged(nameof(FirstName)); } }
		public string	LastName	{ get => lastName;		set { lastName		= value; OnPropertyChanged(nameof(LastName)); } }
		public int		FiscalNumber{ get => fiscalNumber;	set { fiscalNumber	= value; OnPropertyChanged(nameof(FiscalNumber)); } }
		public string	Address		{ get => address;		set { address		= value; OnPropertyChanged(nameof(Address)); } }

		public Person() { }

		public Person(Person old)
		{
			Id = old.Id;
			FirstName = old.FirstName;
			LastName = old.LastName;
			FiscalNumber = old.FiscalNumber;
			Address = old.Address;
		}
	}
}
