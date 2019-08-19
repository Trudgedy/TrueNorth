using Library.Data.Entities;
using System.Linq;

namespace Library.Data
{
	public partial interface IDataRepository<T> where T : BaseEntity
	{
		T GetById(object id);
		void Insert(T entity);
		void Update(T entity);
		void Delete(T entity);
		IQueryable<T> Table { get; }

        
    }
}
