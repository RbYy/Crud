using CrudLib;
using CrudLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject1
{

	public class TestServices<T> : IDataActions<T> where T: BaseModel
	{
		public TestServices(IEnumerable<T> fixtures)
		{
			TestStorageDB = fixtures as List<T>;
		}

		/// <summary>
		/// Replaces a database for testing 
		/// </summary>
		public List<T> TestStorageDB { get; set; }

		// I think it's ok if these tasks run synchronously since they don't access database
		public async Task<T> Create(T item)
		{
			// Autogenerate a new Id
			item.Id = TestStorageDB.Max(p => p.Id) + 1;
			TestStorageDB.Add(item);
			return item;
		}

		public async Task<bool> Delete(int id)
		{
			return TestStorageDB.RemoveAll(item => item.Id == id) == 0
				? false
				: true;
		}

		public async Task<T> Get(int id) => TestStorageDB.FirstOrDefault( p => p.Id == id);

		public async Task<IEnumerable<T>> GetAll() => TestStorageDB ;

		public async Task<T> Update(int id, T item) => TestStorageDB[TestStorageDB.FindIndex(p => p.Id == id)] = item;
	}
}
