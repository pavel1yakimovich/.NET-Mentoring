using EntityFrameworkTask.ORM;
using System;
using System.Linq;

namespace EntityFrameworkTask
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Northwind())
            {
                var orders = context.Orders.Where(o => o.Order_Details.Any(od => od.Product.CategoryID == 1));
                foreach (var order in orders)
                {
                    Console.Write($"OrderId: {order.OrderID} Has products: ");
                    foreach (var orderDetail in order.Order_Details)
                    {
                        Console.Write($"{orderDetail.Product.ProductName} ");
                    }
                    Console.WriteLine($"Customer: {order.Customer.ContactName}");
                }
                Console.ReadKey();
            }
        }
    }
}
