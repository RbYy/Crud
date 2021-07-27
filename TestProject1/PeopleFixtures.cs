using CrudLib.Models;
using System.Collections.Generic;

namespace TestProject1
{
	class PeopleFixtures
	{
		public static List<Person> TestData => new()
		{
			new() { Id = 5, FiscalNumber = 12345678, FirstName = "Micka", LastName = "Županova", Address = "Nekje 3" },
			new() { Id = 9, FiscalNumber = 87654321, FirstName = "Martin", LastName = "Krpan", Address = "Bloke" },
			new() { Id = 1, FiscalNumber = 65230000, FirstName = "Muca", LastName = "Maca", Address = "Naslov 4" },
			new() { Id = 7, FiscalNumber = 10025458, FirstName = "Peter", LastName = "Klepec", Address = "Naslov 5" },
			new() { Id = 8, FiscalNumber = 00264642, FirstName = "Mojca", LastName = "Pokrajculja", Address = "Pisker 7" },
			new() { Id = 2, FiscalNumber = 68418648, FirstName = "Julija", LastName = "Primicova", Address = "Ljubljana" },
			new() { Id = 4, FiscalNumber = 61544817, FirstName = "Zvezdica", LastName = "Zaspanka", Address = "Nebo 8" },
		};

	}
}
