using Crud.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Crud
{
	internal class PeopleDBActions : GenericDBActions<Person>
	{
		public PeopleDBActions(PeopleDbContextFactory contextFactory) : base(contextFactory)
		{ }

		public async Task<Person> GetByFiscal(int fiscal)
		{
			using (PeopleDbContext context = peopleDbContextFactory.CreateDbContext(new string[] { }))
			{ 
				return await context
					.Set<Person>()
					.FirstOrDefaultAsync(i => i.FiscalNumber == fiscal);
			}
		}
	}
}
