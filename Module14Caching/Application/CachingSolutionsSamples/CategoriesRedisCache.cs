using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindLibrary;
using StackExchange.Redis;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace CachingSolutionsSamples
{
	class CategoriesRedisCache : ICache
	{
		private ConnectionMultiplexer redisConnection;
		string prefix = "Cache_Categories";
		DataContractSerializer serializer = new DataContractSerializer(
			typeof(IEnumerable<Category>));

		public CategoriesRedisCache(string hostName)
		{
			redisConnection = ConnectionMultiplexer.Connect(hostName);
		}

		public IEnumerable<Category> Get(string forUser)
		{
			var db = redisConnection.GetDatabase();
			byte[] s = db.StringGet(prefix + forUser);
			if (s == null)
				return null;

			return (IEnumerable<Category>)serializer
				.ReadObject(new MemoryStream(s));

		}

		public void Set(string forUser, IEnumerable<Category> categories)
		{
			var db = redisConnection.GetDatabase();
			var key = prefix + forUser;

			if (categories == null)
			{
				db.StringSet(key, RedisValue.Null);
			}
			else
			{
				var stream = new MemoryStream();
				serializer.WriteObject(stream, categories);
				db.StringSet(key, stream.ToArray());
			}
		}

	    public IEnumerable<Region> GetRegions(string forUser)
	    {
	        throw new NotImplementedException();
	    }

	    public void SetRegions(string forUser, IEnumerable<Region> regions)
	    {
	        throw new NotImplementedException();
	    }

	    public IEnumerable<Supplier> GetSuppliers(string forUser)
	    {
	        throw new NotImplementedException();
	    }

	    public void SetSuppliers(string forUser, IEnumerable<Supplier> suppliers)
	    {
	        throw new NotImplementedException();
	    }
	}
}
