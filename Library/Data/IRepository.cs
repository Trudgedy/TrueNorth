using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Library.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        ICollection<TEntity> List();

        IQueryable<TEntity> Collection();

        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(int id);
    }
}

