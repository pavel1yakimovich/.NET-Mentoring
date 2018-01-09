using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
	public interface ICache
	{
		IEnumerable<Category> Get(string forUser);
		void Set(string forUser, IEnumerable<Category> categories);
	    IEnumerable<Region> GetRegions(string forUser);
	    void SetRegions(string forUser, IEnumerable<Region> regions);
	    IEnumerable<Supplier> GetSuppliers(string forUser);
	    void SetSuppliers(string forUser, IEnumerable<Supplier> suppliers);
    }
}
