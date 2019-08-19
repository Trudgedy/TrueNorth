using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library.Data;
using Library.Data.Entities.Test;
using Library.Services.Caching;

namespace Library.Services
{

	public class TestService : ITestService
	{
		#region Constants
		private const string KEY_PATTERN = "Cache.TestClass";
		private const string KEY_GET_ALL = "Cache.TestClass.GetAll({0},{1})";
		private const string KEY_GET_BY_ID = "Cache.TestClass.GetById({0})";
		#endregion

		#region Fields
		protected IDataRepository<TestClass> _testClassRepo;
		protected IDataContext _db;
		ICacheManager _cacheManager;
		#endregion

		#region Constructor
		public TestService(IDataRepository<TestClass> EntityTypeNameRepo, IDataContext db, ICacheManager cacheManager)
		{
			_testClassRepo = EntityTypeNameRepo;
			_db = db;
			_cacheManager = cacheManager;
		}
		#endregion

		public List<TestClass> GetAll(int page = 0, int pageSize = Int32.MaxValue)
		{
			string key = String.Format(KEY_GET_ALL, page, pageSize);
			return _cacheManager.Get<List<TestClass>>(key, 10, () =>
			{
				//Filter
				var result = _testClassRepo.Table;

				//sorting 
				result = result.OrderBy(t => t.Id);

				//paging
				result = result.Skip(page * pageSize).Take(pageSize);

				//return
				return result.ToList();
			});
		}

		public TestClass GetById(int id)
		{
			string key = String.Format(KEY_GET_BY_ID, id);
			return _cacheManager.Get<TestClass>(key, 10, () =>
			{
				//Filter
				return _testClassRepo.Table.FirstOrDefault(r => r.Id == id);
			});
		}

		public void Insert(TestClass testClass)
		{
			testClass.CreatedOn = DateTime.Now;
			testClass.UpdatedOn = DateTime.Now;
			_testClassRepo.Insert(testClass);
			_cacheManager.RemoveByPattern(KEY_PATTERN);
		}

		public void Update(TestClass testClass)
		{
			testClass.UpdatedOn = DateTime.Now;
			_testClassRepo.Update(testClass);
			_cacheManager.RemoveByPattern(KEY_PATTERN);
		}

		public void Delete(TestClass testClass)
		{
			_testClassRepo.Delete(testClass);
			_cacheManager.RemoveByPattern(KEY_PATTERN);
		}
	}
}
