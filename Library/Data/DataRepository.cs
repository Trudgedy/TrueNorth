using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Library.Data
{
	public partial class DataRepository<T> : IDataRepository<T> where T : BaseEntity
	{
		#region Fields
		protected readonly IDataContext _db;
		protected DbSet<T> _set;
		#endregion

		#region Constructor
		/// <summary>
		/// Constrcuts a new instance of a data repository
		/// </summary>
		/// <param name="context"></param>
		public DataRepository(IDataContext context)
		{
			this._db = context;
		}
		#endregion

		/// <summary>
		/// Fetch an entity from the repository by id
		/// </summary>
		/// <param name="id">The id of the entity</param>
		/// <returns>Returns an entity or null"/></returns>
		public virtual T GetById(object id)
		{
			return this.Entities.Find(id);
		}

		/// <summary>
		/// Insert a new entity into the repository
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Insert(T entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			this.Entities.Add(entity);
			try
			{
				this._db.SaveChanges();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", ex.Message, ex.InnerException);
			}

		}

		/// <summary>
		/// Update an existing entity in the repository
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Update(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			//Look for the entity in the object context (if is already exists)
			var e = FindEntity(entity);

			//The entity is not in the object context, attach it
			if (e == null)
			{
				Entities.Attach(entity);
				Throw(_db).Entry(entity).State = EntityState.Modified;
			}

			//The entity is already in the object context, set the values for it
			else
			{
				Throw(_db).Entry(e).CurrentValues.SetValues(entity);
			}

			//Attempt to set the ModifiedOnUtc value of the entity
			var modifiedOn = typeof(T).GetProperty("ModifiedOnUtc");
			modifiedOn?.SetValue(entity, DateTime.UtcNow, null);

			//Save to the database
			_db.SaveChanges();
		}

		/// <summary>
		/// Gets the primary key values form the entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		private T FindEntity(T entity)
		{
			//Attempt to get the primary key from the object
			{
				var pkName = $"{typeof(T).Name}Id";
				var pkProperty = typeof(T).GetProperty(pkName);
				var pkValue = pkProperty?.GetValue(entity);
				if (pkValue != null)
				{
					return Entities.Find(pkValue);
				}
			}

			//No entity found
			return null;
		}

		/// <summary>
		/// Deletes an existing entity from the repository
		/// </summary>
		/// <param name="entity"></param>
		public virtual void Delete(T entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (Entities.Local.FirstOrDefault(e => e == entity) == null)
				Entities.Attach(entity);

			//Delete the entity
			Entities.Remove(entity);
			_db.SaveChanges();
		}

		/// <summary>
		/// Acces the entire entity table
		/// </summary>
		public virtual IQueryable<T> Table
		{
			get
			{
				return this.Entities;
			}
		}

		/// <summary>
		/// Access the entire set of entities
		/// </summary>
		protected virtual DbSet<T> Entities
		{
			get
			{
				if (_set == null) _set = _db.Set<T>();
				return _set;
			}
		}

		/// <summary>
		/// Throw the interface to a database context
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		private DbContext Throw(IDataContext context)
		{
			return (DbContext)(context as DbContext);
		}
	}
}
