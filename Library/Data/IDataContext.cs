using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace Library.Data
{
	public interface IDataContext
	{
		DbSet<T> Set<T>() where T : BaseEntity;
		int SaveChanges();
		bool DatabaseExists();
		void ExecuteSql(String sql);
	}
}
