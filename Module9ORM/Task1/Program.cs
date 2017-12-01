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
            }
            #endregion

            Console.ReadKey();
        }
    }
}
