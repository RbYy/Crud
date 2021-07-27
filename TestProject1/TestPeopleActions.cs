using CrudLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject1
{
	// This is less generic, since fiscal number apply only to people
	public class TestPeopleActions<T> : TestServices<T>, IPeopleDataActions<T> where T : Person
	{
		public TestPeopleActions(IEnumerable<T> fixtures) : base(fixtures) { }

		public async Task<T> GetByFiscal(int fiscal) => TestStorageDB.FirstOrDefault(i => i.FiscalNumber == fiscal);
	}
}
