using CrudLib.Models;
using CrudLib.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestProject1
{

	[TestClass]
	public class ViewModelTest
	{
		// Assigning to vm.InputPerson simulates editing the UI form
		// Assigning to vm.SelectedPerson simulates selecting an item from the list (datagrid)
		[TestMethod]
		public void GetAll_count()
		{
			MainViewModel vm = new MainViewModel(new TestPeopleActions<Person>(PeopleFixtures.TestData));
			vm.DataService.GetAll().Result.ToList();
			// Test retrieving all from storage
			Assert.AreEqual(PeopleFixtures.TestData.Count, vm.PeopleList.Count);
		}

		[TestMethod]
		public void ItemInsert()
		{
			MainViewModel vm = new MainViewModel(new TestPeopleActions<Person>(PeopleFixtures.TestData));
			vm.DataService.GetAll().Result.ToList();
			// we are adding super man
			vm.InputPerson = new Person 
			{
				FirstName = "Super",
				LastName = "Man",
				FiscalNumber = 96325874,
				Address = "Crypton" 
			};
			// *** ADD AN ITEM
			vm.Create(new());

			// Is item inserted into vm collection
			Assert.AreEqual(PeopleFixtures.TestData.Count +1, vm.PeopleList.Count);
			//is storage updated too
			CollectionAssert.AreEqual((vm.DataService as TestServices<Person>).TestStorageDB, vm.PeopleList);
			// is it the right item
			Assert.IsNotNull(vm.PeopleList.FirstOrDefault(p => p.FirstName == "Super"));
		}

		[TestMethod]
		public void ItemInsert_FiscalAlreadyExist()
		{
			MainViewModel vm = new MainViewModel(new TestPeopleActions<Person>(PeopleFixtures.TestData));
			var iitialCollection = vm.DataService.GetAll().Result.ToList();
			// we are adding super man
			vm.InputPerson = new Person
			{
				FirstName = "Super",
				LastName = "Man",
				FiscalNumber = 12345678, // A person with this fiscal number (Županova Micka) already exists in the initial collection
				Address = "Crypton"
			};
			// *** ADD AN ITEM
			vm.Create(new());

			// after create attempt check the colection again
			vm.DataService.GetAll().Result.ToList();

			Assert.AreEqual(PeopleFixtures.TestData.Count, vm.PeopleList.Count);
			// after create, collection same as initial data
			CollectionAssert.AreEqual(iitialCollection, vm.PeopleList);
			//is storage the same too
			CollectionAssert.AreEqual((vm.DataService as TestServices<Person>).TestStorageDB, vm.PeopleList);
			// is it the right item
			Assert.IsNull(vm.PeopleList.FirstOrDefault(p => p.FirstName == "Super"));
		}

		[TestMethod]
		public void ItemRemove()
		{
			MainViewModel vm = new MainViewModel(new TestPeopleActions<Person>(PeopleFixtures.TestData));
			// Let's try to remove Peter Klepec
			vm.DataService.GetAll().Result.ToList();
			vm.SelectedPerson = vm.PeopleList.FirstOrDefault(p => p.FirstName == "Peter");

			//***** EXECUTE THE ACTION TO REMOVE ITEM
			vm.Delete(new());
			
			// is Peter removed from vm collection
			Assert.AreEqual(PeopleFixtures.TestData.Count - 1, vm.PeopleList.Count);
			//Is removed also from "storage"
			CollectionAssert.AreEqual((vm.DataService as TestServices<Person>).TestStorageDB, vm.PeopleList);
			//Is the right person removed
			Assert.IsNull(vm.PeopleList.FirstOrDefault(p => p.FirstName == "Peter"));
		}

		[TestMethod]
		public void ItemRemove_IdNotExist_nobodySelected()
		{
			MainViewModel vm = new MainViewModel(new TestPeopleActions<Person>(PeopleFixtures.TestData));
			var initialCollection = vm.DataService.GetAll().Result.ToList();
			// Let's try to remove some person with id=77, which does not exist
			//witout selecting anybody from the list
			vm.SelectedPerson = null;
			vm.InputPerson.Id = 77;

			//***** EXECUTE THE ACTION TO REMOVE ITEM
			vm.Delete(new());
			// no item should be removed from collection
			Assert.AreEqual(PeopleFixtures.TestData.Count, vm.PeopleList.Count);
			// after remove attempt, collection same as initial data
			CollectionAssert.AreEqual(initialCollection, vm.PeopleList);
			// also the "storage" should have remained the same
			CollectionAssert.AreEqual((vm.DataService as TestServices<Person>).TestStorageDB, vm.PeopleList);
		}

		[TestMethod]
		public void ItemUpdate()
		{
			MainViewModel vm = new MainViewModel(new TestPeopleActions<Person>(PeopleFixtures.TestData));
			// Let's try to change Muca's last name. From "Maca" to "Copatarica"
			vm.DataService.GetAll().Result.ToList();
			vm.SelectedPerson = vm.PeopleList.FirstOrDefault(p => p.FirstName == "Muca");
			vm.InputPerson.LastName = "Copatarica";
			//***** UPDATE ITEM, change last name
			vm.Update(new());

			// the vm collection length should remain the same
			Assert.AreEqual(PeopleFixtures.TestData.Count, vm.PeopleList.Count);
			// Is updated also in "storage"
			CollectionAssert.AreEqual((vm.DataService as TestServices<Person>).TestStorageDB, vm.PeopleList);
			// Is the right person updated
			Assert.IsNotNull(vm.PeopleList.FirstOrDefault(p => p.FirstName == "Muca" && p.LastName == "Copatarica"));
		}

		[TestMethod]
		public void ItemUpdate_FiscalAlreadyExists()
		{
			// Fiscal number must be unique
			MainViewModel vm = new MainViewModel(new TestPeopleActions<Person>(PeopleFixtures.TestData));
			var initialCollection = vm.DataService.GetAll().Result.ToList();
			//select a person (Muca). Her fiscal is 65230000
			vm.SelectedPerson = vm.PeopleList.FirstOrDefault(p => p.FirstName == "Muca");

			// try to change het fiscal number to 12345678
			// A person with this fiscal number (Županova Micka) already exists in the initial collection
			vm.InputPerson.FiscalNumber = 12345678;

			//***** UPDATE ITEM, change fiscal
			vm.Update(new());

			// after update attempt check the colection again
			vm.DataService.GetAll().Result.ToList();

			// The "database" and vm.PeopleList must remain the same
			Assert.AreEqual(PeopleFixtures.TestData.Count, vm.PeopleList.Count);
			// after update attempt, is collection the same as initial data
			CollectionAssert.AreEqual(initialCollection, vm.PeopleList);
			//is storage the same too
			CollectionAssert.AreEqual((vm.DataService as TestServices<Person>).TestStorageDB, vm.PeopleList);
			// is it the right item
			Assert.IsNull(vm.PeopleList.FirstOrDefault(p => p.FirstName == "Super"));
		}

		[TestMethod]
		public void Get_byFiscal()
		{
			MainViewModel vm = new MainViewModel(new TestPeopleActions<Person>(PeopleFixtures.TestData));
			// select some person
			vm.SelectedPerson = vm.PeopleList.FirstOrDefault(p => p.FirstName == "Muca");
			vm.InputPerson.FiscalNumber = 12345678; // Input a fiscal number, we know belongs to Županova Micka

			// *** GET ITEM BY FISCAL
			vm.GetByFiscal(new()); 
			// if found, it should have been asigned to vm.SelectedPerson
			Assert.AreEqual("Micka", vm.SelectedPerson.FirstName);
		}

		[TestMethod]
		public void Get_byFiscal_notFound()
		{
			MainViewModel vm = new MainViewModel(new TestPeopleActions<Person>(PeopleFixtures.TestData));
			vm.SelectedPerson = vm.PeopleList.FirstOrDefault(p => p.FirstName == "Muca");
			vm.InputPerson.FiscalNumber = 666; // A person with this fiscal does not exist
			// *** GET ITEM BY FISCAL
			vm.GetByFiscal(new());

			// SelectedPerson shouldn't have changed
			Assert.AreEqual("Muca", vm.SelectedPerson.FirstName);
		}
	}
}
