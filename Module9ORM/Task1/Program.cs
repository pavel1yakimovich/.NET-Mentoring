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
            List<Product> products;
            List<Category> categories;
            List<Supplier> suppliers;

            using (var db = new DbNorthwind())
            {
                var ed = db.MappingSchema.GetEntityDescriptor(typeof(Product));
                products = db.Products.LoadWith(r => r.Category).LoadWith(r => r.Supplier).ToList();
                categories = db.Categories.ToList();
                suppliers = db.Suppliers.ToList();
            }

            foreach (var item in products)
            {
                Console.WriteLine($"Product: {item.Name} Category: {item.Category.Name} Supplier: {item.Supplier.Name}");
            }
            #endregion

            Console.ReadKey();
        }
    }
}
