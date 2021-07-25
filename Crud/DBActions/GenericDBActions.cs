using Crud.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud
{

	public class GenericDBActions<T> : IDataActions<T> where T : BaseModel
	{
		protected readonly PeopleDbContextFactory peopleDbContextFactory;

		public GenericDBActions(PeopleDbContextFactory contextFactory)
		{
			peopleDbContextFactory = contextFactory;
		}

		public async Task<T> Create(T item)
		{
			using (PeopleDbContext context = peopleDbContextFactory.CreateDbContext(new string[] { }))
			{
				var created = await context
					.Set<T>()
					.AddAsync(item);
				await context.SaveChangesAsync();
				return created.Entity;
			}
		}

		public async Task<bool> Delete(int id)
		{
			using (PeopleDbContext context = peopleDbContextFactory.CreateDbContext(new string[] { }))
			{
				T item = await context.Set<T>().FirstOrDefaultAsync( i => i.Id == id);
				if (item == null)
				{
					return false;
				}
				context
					.Set<T>()
					.Remove(item);
				await context.SaveChangesAsync();
				return true;
			}
		}

		public async Task<T> Get(int id)
		{
			using (PeopleDbContext context = peopleDbContextFactory.CreateDbContext(new string[] { }))
			{
				return await context
					.Set<T>()
					.FirstOrDefaultAsync(i => i.Id == id);
			}
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			using (PeopleDbContext context = peopleDbContextFactory.CreateDbContext(new string[] { }))
			{
				return await context.Set<T>().ToListAsync();
			}
			
		}

		public async Task<T> Update(T item)
		{
			using (PeopleDbContext context = peopleDbContextFactory.CreateDbContext(new string[] { }))
			{
				context
					.Set<T>()
					.Update(item);
				await context.SaveChangesAsync();
				return item;
			}
		}
	}
}
