using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region task 2
            Task3_3();
            #endregion

            Console.ReadKey();
        }

        static void Task2()
        {
            using (var db = new DbNorthwind())
            {
                var ed = db.MappingSchema.GetEntityDescriptor(typeof(Product));
                var products = db.Products.LoadWith(r => r.Category).LoadWith(r => r.Supplier).ToList();
                var employees = db.Employees.Join(db.EmployeeTerritories, e => e.EmployeeID, et => et.EmployeeID, (e, et) => new { e, et })
                    .Join(db.Territories, et => et.et.TerritoryID, t => t.TerritoryID, (et, t) => new { et, t })
                    .Join(db.Regions, t => t.t.RegionID, r => r.RegionID, (t, r) => new { t, r })
                    .GroupBy(r => r.t.et.e.FirstName + r.t.et.e.LastName, r => r.r.Description, (r, e) => new { Id = r, Region = e.FirstOrDefault() });

                foreach (var item in products)
                {
                    Console.WriteLine($"Product: {item.Name} Category: {item.Category.Name} Supplier: {item.Supplier.Name}");
                }

                foreach (var item in employees)
                {
                    Console.WriteLine($"Employee: {item?.Id} Region: {item?.Region}");
                }

                var employees2 = db.Employees.Join(db.EmployeeTerritories, e => e.EmployeeID, et => et.EmployeeID, (e, et) => new { e, et })
                    .Join(db.Territories, et => et.et.TerritoryID, t => t.TerritoryID, (et, t) => new { et, t })
                    .Join(db.Regions, t => t.t.RegionID, r => r.RegionID, (t, r) => new { t, r })
                    .GroupBy(r => r.r.Description, r => r.t.et.e.EmployeeID, (r, e) => new { Region = r, Employees = e.Count() });

                foreach (var item in employees2)
                {
                    Console.WriteLine($"Region: {item?.Region} Employees: {item?.Employees}");
                }

                var employees3 = db.Employees.Join(db.Orders, e => e.EmployeeID, o => o.EmployeeID, (e, o) => new { e, o })
                    .Join(db.Shippers, o => o.o.ShipperID, s => s.ShipperID, (o, s) => new { o, s })
                    .GroupBy(s => s.o.e.EmployeeID, s => s.s.CompanyName, (e, s) => new { Employee = e, Shippers = s.Distinct() });

                foreach (var item in employees3)
                {
                    Console.Write($"Employee: {item.Employee} Shippers: ");
                    foreach (var shipper in item.Shippers)
                    {
                        Console.Write($"{shipper}, ");
                    }
                    Console.WriteLine();
                }
            }
        }

        static void Task3_1()
        {
            var employee = new Employee() { FirstName = "Pasha", LastName = "Yakimovich" };
            using (var db = new DbNorthwind())
            {
                employee.EmployeeID = Convert.ToInt32(db.InsertWithIdentity(employee));

                db.EmployeeTerritories.Insert(() => new EmployeeTerritories() { EmployeeID = employee.EmployeeID, TerritoryID = 19713 });
            }
        }

        static void Task3_2()
        {
            using (var db = new DbNorthwind())
            {
                var product = db.Products.Where(p => p.ProductID == 2).Set(p => p.CategoryID, 1).Update();
            }
        }

        static void Task3_3()
        {
            using (var db = new DbNorthwind())
            {
                var products = new List<Product>
                {
                    new Product { Name = "First", SupplierID = 1, CategoryID = 100 },
                    new Product { Name = "Second", SupplierID = 100, CategoryID = 2 }
                };

                foreach (var product in products)
                {
                    if (!db.Suppliers.Where(s => s.SupplierID == product.SupplierID).Any())
                    {
                        var supplier = new Supplier { Name = "New company" };
                        supplier.SupplierID = Convert.ToInt32(db.InsertWithIdentity(supplier));
                        product.SupplierID = supplier.SupplierID;
                    }

                    if(!db.Categories.Where(c => c.CategoryID == product.CategoryID).Any())
                    {
                        var category = new Category { Name = "New category" };
                        category.CategoryID = Convert.ToInt32(db.InsertWithIdentity(category));
                        product.CategoryID = category.CategoryID;
                    }

                    db.Insert(product);
                }
            }
        }
    }
}
