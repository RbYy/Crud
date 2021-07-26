using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudLib
{
	public class PeopleDbContextFactory : IDesignTimeDbContextFactory<PeopleDbContext>
	{
		public PeopleDbContext CreateDbContext(string[] args)
		{
			DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<PeopleDbContext>();

			optionsBuilder.UseSqlite(@"DataSource=people.db;");

			return new PeopleDbContext(optionsBuilder.Options);
		}
	}
}

