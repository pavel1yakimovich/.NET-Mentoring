using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
	public class CategoriesManager
	{
		private ICache cache;

		public CategoriesManager(ICache cache)
		{
			this.cache = cache;
		}

		public IEnumerable<Category> GetCategories()
		{
			Console.WriteLine("Get Categories");

			var user = Thread.CurrentPrincipal.Identity.Name;
			var categories = cache.Get(user);

			if (categories == null)
			{
				Console.WriteLine("From DB");

				using (var dbContext = new Northwind())
				{
					dbContext.Configuration.LazyLoadingEnabled = false;
					dbContext.Configuration.ProxyCreationEnabled = false;
					categories = dbContext.Categories.ToList();
					cache.Set(user, categories);
				}
			}

			return categories;
		}

	    public IEnumerable<Region> GetRegions()
	    {
	        Console.WriteLine("Get Regions");

	        var user = Thread.CurrentPrincipal.Identity.Name;
	        var regions = cache.GetRegions(user);

	        if (regions == null)
	        {
	            Console.WriteLine("From DB");

	            using (var dbContext = new Northwind())
	            {
	                dbContext.Configuration.LazyLoadingEnabled = false;
	                dbContext.Configuration.ProxyCreationEnabled = false;
	                regions = dbContext.Regions.ToList();
	                cache.SetRegions(user, regions);
	            }
	        }

	        return regions;
	    }

	    public IEnumerable<Supplier> GetSuppliers()
	    {
	        Console.WriteLine("Get Suppliers");

	        var user = Thread.CurrentPrincipal.Identity.Name;
	        var suppliers = cache.GetSuppliers(user);

	        if (suppliers == null)
	        {
	            Console.WriteLine("From DB");

	            using (var dbContext = new Northwind())
	            {
	                dbContext.Configuration.LazyLoadingEnabled = false;
	                dbContext.Configuration.ProxyCreationEnabled = false;
	                suppliers = dbContext.Suppliers.ToList();
	                cache.SetSuppliers(user, suppliers);
	            }
	        }

	        return suppliers;
	    }
    }
}
