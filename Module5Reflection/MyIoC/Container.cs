using Fasterflect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyIoC
{
    [TestClass]
	public class Container
	{
        private Dictionary<Type, Type> container = new Dictionary<Type, Type>();

		public void AddAssembly(Assembly assembly)
		{
            var exportTypes = assembly.TypesWith<ExportAttribute>().Select(t => new
            {
                type = t,
                exportType = t.Attribute<ExportAttribute>().Contract
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
            if (type.GetCustomAttribute<ImportConstructorAttribute>() != null)
            {
                var ctor = type.Constructors().Single();
                var parameters = ctor.Parameters();
                var parametersObjects = new object[parameters.Count()];

                for (int i = 0; i < parameters.Count(); i++)
                {
                    parametersObjects[i] = container[parameters[i].ParameterType].CreateInstance();
                }
                return ctor.Invoke(parametersObjects);
            }
            else
            {
                var ctor = type.Constructor();
                var properties = type.PropertiesWith(Flags.InstancePublic, typeof(ImportAttribute));
                
                var result = ctor.Invoke(null);
                foreach(var prop in properties)
                {
                    result.SetPropertyValue(prop.Name, container[prop.PropertyType].CreateInstance());
                }

                return result;
            }
        }

        public T CreateInstance<T>() => (T)this.CreateInstance(typeof(T));

        [TestMethod]
		public void Sample()
		{
			var container = new Container();
            //container.AddAssembly(Assembly.GetExecutingAssembly());

            //container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL2>();
        }
	}
}
