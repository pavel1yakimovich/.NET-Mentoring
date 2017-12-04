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
        public ITable<Employee> Employees { get { return GetTable<Employee>(); } }
        public ITable<EmployeeTerritories> EmployeeTerritories { get { return GetTable<EmployeeTerritories>(); } }
        public ITable<Territory> Territories { get { return GetTable<Territory>(); } }
        public ITable<Region> Regions { get { return GetTable<Region>(); } }
        public ITable<Order> Orders { get { return GetTable<Order>(); } }
        public ITable<Shipper> Shippers { get { return GetTable<Shipper>(); } }
        public ITable<OrderDetails> OrderDetails { get { return GetTable<OrderDetails>(); } }
    }
}
