using System.Threading.Tasks;

namespace CrudLib.Models
{
	public interface IPeopleDataActions<T> : IDataActions<T>
	{
		public Task<T> GetByFiscal(int fiscal);
	}
}
