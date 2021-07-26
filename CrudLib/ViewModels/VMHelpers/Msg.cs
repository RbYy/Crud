using CrudLib.Models;

namespace CrudLib.ViewModels
{
	public class Msg
	{
		public Person selectedPerson { get; set; } = new();

		public Msg(Person p)
		{
			selectedPerson = p;
		}
		public string SelectionRequired => "Za posodobitev je potrebno izbrati osebo s seznama.";
		public string DeleteFail		=> $"Odstranitev osebe {selectedPerson?.FirstName} {selectedPerson?.LastName} ni bila uspešna.";
		public string Deleted			=> $"Oseba {selectedPerson?.FirstName} {selectedPerson?.LastName} je odstranjena.";
		public string Updated			=> $"Oseba {selectedPerson?.FirstName} {selectedPerson?.LastName} je posodobljena.";
		public string Created			=> $"Dodana oseba {selectedPerson?.FirstName} {selectedPerson?.LastName}.";
		public string AlreadyExists		=> "Oseba s to davčno številko že obstaja";
		public string NotExist			=> $"Oseba z davčno številko {selectedPerson?.FiscalNumber} ne obstaja.";
		public string Found				=> $"Oseba z davčno številko {selectedPerson?.FiscalNumber} je najdena.";
	}
}
