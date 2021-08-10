using CrudLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudLib
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
			using PeopleDbContext context = peopleDbContextFactory.CreateDbContext();
			var created = await context.Set<T>().AddAsync(item);
			_ = await context.SaveChangesAsync();
			return created.Entity;
		}

		public async Task<bool> Delete(int id)
		{
			using PeopleDbContext context = peopleDbContextFactory.CreateDbContext();
			T item = await context.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
			if (item == null)
			{
				return false;
			}
			_ = context.Set<T>().Remove(item);
			_ = await context.SaveChangesAsync();
			return true;
		}

		public async Task<T> Get(int id)
		{
			using PeopleDbContext context = peopleDbContextFactory.CreateDbContext();
			return await context.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			using PeopleDbContext context = peopleDbContextFactory.CreateDbContext();
			return await context.Set<T>().ToListAsync();
		}

		public async Task<T> Update(int id, T item)
		{
			using PeopleDbContext context = peopleDbContextFactory.CreateDbContext();
			_ = context.Set<T>().Update(item);
			_ = await context.SaveChangesAsync();
			return item;
		}
	}
}
