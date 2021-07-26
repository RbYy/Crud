using CrudLib;
using CrudLib.Models;
using CrudLib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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

	class TestServices<T> : IDataActions<T> where T: BaseModel
	{
		List<T> TestData = PeopleFixtures.TestData as List<T>;
		public Task<T> Create(T item)
		{
			// Autogenerate a new Id
			item.Id = TestData.Max(p => p.Id) + 1;
			TestData.Add(item);
			return new Task<T>(() => item);
		}

		public Task<bool> Delete(int id)
		{
			TestData.RemoveAt(TestData.FindIndex(p => p.Id == id));
			return new Task<bool>(() => true);
		}

		public Task<T> Get(int id) => 
			new Task<T>(() => TestData.FirstOrDefault( p => p.Id == id));

		public Task<IEnumerable<T>> GetAll() => 
			new Task<IEnumerable<T>>(()=>TestData );

		public Task<T> Update(int id, T item) => 
			new Task<T>(() => TestData[TestData.FindIndex(p => p.Id == id)] = item);
	}

	[TestClass]
	public class UnitTest1
	{
		//public IDataActions<Person> DataService { get; set; }

		[TestMethod]
		public void TestMethod1()
		{
			MainViewModel vm = new MainViewModel(new TestServices<Person>());
			vm.PeopleList = new ObservableCollection<Person>(vm.DataService.GetAll().Result);
			Assert.AreEqual<IEnumerable<Person>>(PeopleFixtures.TestData, vm.PeopleList);
		}
	}
}
