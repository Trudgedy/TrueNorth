using System;
using System.Collections.Generic;
using Library.Data.Entities.Test;

namespace Library.Services
{

	public interface ITestClassService
	{
		List<TestClass> GetAll(Int32 page = 0, Int32 pageSize = Int32.MaxValue);

		TestClass GetById(Int32 id);

		void Insert(TestClass testClass);
		void Update(TestClass testClass);
		void Delete(TestClass testClass);
	}
}
