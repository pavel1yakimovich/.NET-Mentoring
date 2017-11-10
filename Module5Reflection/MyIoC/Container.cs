using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
	public class Container
	{
        private Dictionary<Type, Type> container = new Dictionary<Type, Type>();

		public void AddAssembly(Assembly assembly)
		{
            var exportTypes = assembly.GetTypes().Where(t => t.GetCustomAttribute<ExportAttribute>() != null)
                .Select(t => new
                {
                    type = t,
                    exportType = t.GetCustomAttribute<ExportAttribute>().Contract
                });

            foreach (var item in exportTypes)
            {
                if (item.exportType != null)
                {
                    this.AddType(item.type, item.exportType);
                }
                else
                {
                    this.AddType(item.type);
                }
            }
        }

		public void AddType(Type type) => this.AddType(type, type);

		public void AddType(Type type, Type baseType) => container.Add(baseType, type);

		public object CreateInstance(Type type)
		{
			return null;
		}

		public T CreateInstance<T>()
		{
            return default(T);
		}


		public void Sample()
		{
			var container = new Container();
			container.AddAssembly(Assembly.GetExecutingAssembly());

			var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
			var customerBLL2 = container.CreateInstance<CustomerBLL>();

			container.AddType(typeof(CustomerBLL));
			container.AddType(typeof(Logger));
			container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));
		}
	}
}
