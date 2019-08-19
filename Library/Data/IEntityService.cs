using Library.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Library.Data
{
    public interface IEntityService<TEntity> where TEntity : class, IEntity
    {
        TEntity Get(int id);

        ICollection<TEntity> List();

        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(int id);

        bool Validate(TEntity entity, ref IDictionary<string, string> validationErrors);
    }
}