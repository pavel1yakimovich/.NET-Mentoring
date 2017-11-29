using LinqToDB;
using LinqToDB.Mapping;
using Task1.Models;

namespace Task1
{
    public class DbNorthwind : LinqToDB.Data.DataConnection
    {
        public DbNorthwind() : base("Northwind") { }

        public ITable<Product> Products { get { return GetTable<Product>(); } }
        public ITable<Category> Categories { get { return GetTable<Category>(); } }
        public ITable<Supplier> Suppliers { get { return GetTable<Supplier>(); } }
    }
}
