using CrudLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudLib
{
	public class PeopleDbContext : DbContext
	{
		public DbSet<Person> People { get; set; }
		public PeopleDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
		{
			Database.EnsureCreated();
		}
	}
}
