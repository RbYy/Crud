using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Models
{
	public interface IDataActions<T>
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> Create(T item);
		Task<T> Get(int id);
		Task<T> Update(T item);
		Task<bool> Delete(int id);
	}
}
