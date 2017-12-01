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
                    .Join(db.Regions, t => t.t.RegionID, r => r.RegionID, (t, r) => new { t, r });
                
                foreach (var item in products)
                {
                    Console.WriteLine($"Product: {item.Name} Category: {item.Category.Name} Supplier: {item.Supplier.Name}");
                }

                //hack
                var empIds = new List<int?>();
                foreach (var item in employees)
                {
                    var empId = item?.t?.et?.e?.EmployeeID;
                    if (!empIds.Contains(empId))
                    {
                        empIds.Add(empId);
                        Console.WriteLine($"Employee: {item?.t?.et?.e?.FirstName} {item?.t?.et?.e?.LastName} Region: {item?.r?.Description}");
                    }
                }
            }
            #endregion

            Console.ReadKey();
        }
    }
}
