using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Extensions.Options;

namespace Library.Services.Caching
{
	/// <summary>
	/// Represents a MemoryCacheCache
	/// </summary>
	public partial class MemoryCacheManager : ICacheManager
	{

		private static List<String> Keys = new List<String>();
		protected static MemoryCache Cache = new MemoryCache(new MemoryCacheOptions { });


		/// <summary>
		/// Gets or sets the value associated with the specifUnable to cast object of type 'System.Collections.Generic.List`1[Libraries.Core.Domain.Customer]' to type 'System.Collections.Generic.ICollection`1[Libraries.Core.Domain.Community.Community]'ied key.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="key">The key of the value to get.</param>
		/// <returns>The value associated with the specified key.</returns>
		public virtual T Get<T>(string key)
		{
			return Cache.Get<T>(key);
		}

		/// <summary>
		/// Adds the specified key and object to the cache.
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="data">Data</param>
		/// <param name="cacheTime">Cache time</param>
		public virtual void Set(string key, object data, int cacheTime)
		{
			if (data == null)
				return;

			Cache.Set(key, data, DateTime.Now + TimeSpan.FromMinutes(cacheTime));
			Keys.Add(key);
		}

		/// <summary>
		/// Gets a value indicating whether the value associated with the specified key is cached
		/// </summary>
		/// <param name="key">key</param>
		/// <returns>Result</returns>
		public virtual bool IsSet(string key)
		{
			var result = Cache.Get(key);
			return result != null;
		}

		/// <summary>
		/// Removes the value with the specified key from the cache
		/// </summary>
		/// <param name="key">/key</param>
		public virtual void Remove(string key)
		{
			Keys.Remove(key);
			Cache.Remove(key);
		}

		/// <summary>
		/// Removes items by pattern
		/// </summary>
		/// <param name="pattern">pattern</param>
		public virtual void RemoveByPattern(string pattern)
		{
			var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
			var keysToRemove = new List<String>();

			foreach (var key in Keys)
				if (regex.IsMatch(key))
					keysToRemove.Add(key);

			foreach (string key in keysToRemove)
			{
				Keys.Remove(key);
				Remove(key);
			}
		}

		/// <summary>
		/// Clear all cache data
		/// </summary>
		public virtual void Clear()
		{
			foreach (var key in Keys)
				Remove(key);

			Keys.Clear();
		}

	}
}