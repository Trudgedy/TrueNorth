using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Library.Data.Entities;
using System.Linq;

namespace Library.Data
{
	public partial class DatabaseContext : DbContext, IDataContext
	{

		#region Constructor
		public DatabaseContext(DbContextOptions<DatabaseContext> options)
			: base(options)
		{ }


		#endregion

		#region Properties
		/// <summary>
		/// Gets the specified entity set from the context
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public new DbSet<T> Set<T>() where T : BaseEntity
		{
			return base.Set<T>();
		}
		#endregion

		#region Methods

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Look for and add the entity configurations to the model builder
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}

		public bool DatabaseExists()
		{
			throw new NotImplementedException();
		}

		public void ExecuteSql(string sql)
		{
			throw new NotImplementedException();
		}

		#endregion

	}
}
