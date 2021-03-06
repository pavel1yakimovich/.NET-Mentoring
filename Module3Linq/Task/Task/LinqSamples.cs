﻿// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		//[Category("Restriction Operators")]
		//[Title("Where - Task 1")]
		//[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		//public void Linq1()
		//{
		//	int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

		//	var lowNums =
		//		from num in numbers
		//		where num < 5
		//		select num;

		//	Console.WriteLine("Numbers < 5:");
		//	foreach (var x in lowNums)
		//	{
		//		Console.WriteLine(x);
		//	}
		//}

		//[Category("Restriction Operators")]
		//[Title("Where - Task 2")]
		//[Description("This sample return return all presented in market products")]

		//public void Linq2()
		//{
		//	var products =
		//		from p in dataSource.Products
		//		where p.UnitsInStock > 0
		//		select p;

		//	foreach (var p in products)
		//	{
		//		ObjectDumper.Write(p);
		//	}
		//}

        [Category("Restriction Operators")]
        [Title("Where : Task 1")]
        [Description("This sample returns all customers who's sum of orders more than some amount")]
        public void Linq1()
	    {
	        var clients = dataSource.Customers.Where(c => c.Orders.Sum(o => o.Total) > 8000);

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }

            clients = dataSource.Customers.Where(c => c.Orders.Sum(o => o.Total) > 10000);

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Join Operators")]
        [Title("Join : Task 2")]
        [Description("This sample returns all suplpliers for customers int his city")]
        public void Linq2()
        {
            var pairs = dataSource.Customers.GroupJoin(dataSource.Suppliers, c => c.City, s => s.City, (c, s) => new { Customer = c, Suppliers = s});
            
            foreach (var p in pairs)
            {
                Console.Write($"Customer: {p.Customer.CustomerID}; suppliers: ");
                foreach (var s in p.Suppliers)
                {
                    Console.Write($"{s.SupplierName}, ");
                }
                Console.WriteLine();
            }

            var newPairs = dataSource.Customers.Select(c => new { customer = c, suppliers = dataSource.Suppliers.Where(s => s.City == c.City) });

            foreach (var p in newPairs)
            {
                Console.Write($"Customer: {p.customer.CustomerID}; suppliers: ");
                foreach (var s in p.suppliers)
                {
                    Console.Write($"{s.SupplierName}, ");
                }
                Console.WriteLine();
            }

        }

	    [Category("Restriction Operators")]
	    [Title("Where : Task 3")]
	    [Description("This sample returns all customers who had orders with total > 5000")]
	    public void Linq3()
	    {
	        var customers = dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > 5000));

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Projection Operators")]
        [Title("Select : Task 4")]
        [Description("This sample returns all customers with date of their first order")]
        public void Linq4()
        {
            var customers =
                dataSource.Customers.Where(c => c.Orders.Any()).Select(c => new
                {
                    Customer = c,
                    Date = c.Orders.Select(o => o.OrderDate).Min()
                });

            foreach (var c in customers)
            {
                Console.WriteLine($"Customer={c.Customer.CustomerID} First order={c.Date.Month}/{c.Date.Year}");
            }
        }

        [Category("Ordering Operators")]
        [Title("Order : Task 5")]
        [Description("This sample returns all customers with date of their first order order by year, month, sum of orders, customer's name")]
        public void Linq5()
        {
            var customers =
                dataSource.Customers.Where(c => c.Orders.Any()).Select(c => new
                    {
                        Customer = c,
                        Date = c.Orders.Select(o => o.OrderDate).Min()
                    })
                    .OrderBy(c => c.Date.Year)
                    .ThenBy(c => c.Date.Month)
                    .ThenByDescending(c => c.Customer.Orders.Sum(o => o.Total))
                    .ThenBy(c => c.Customer.CustomerID);

            foreach (var c in customers)
            {
                Console.WriteLine($"Customer={c.Customer.CustomerID} First order={c.Date.Month}/{c.Date.Year} Sum={c.Customer.Orders.Sum(o => o.Total)}");
            }
        }

        [Category("Ordering Operators")]
        [Title("Order : Task 6")]
        [Description("This sample returns all customers without region or mobile operator and non-digit postal code")]
        public void Linq006()
        {
            var number = 0;
            var customers = dataSource.Customers.Where(c => String.IsNullOrWhiteSpace(c.Region) || c.Phone.First() != '(' || !int.TryParse(c.PostalCode, out number));

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Grouping Operators")]
        [Title("Group : Task 7")]
        [Description("This sample groups products")]
        public void Linq007()
        {
            var products = dataSource.Products.GroupBy(x => x.Category, (key, g1) =>
                                g1.GroupBy(x => x.UnitsInStock));

            products.SelectMany(p => p).Last().OrderBy(o => o.UnitPrice);

            foreach (var p in products.SelectMany(o => o))
            {
                foreach (var i in p.Select(t => t))
                {
                    ObjectDumper.Write(i);
                }
            }
        }

        [Category("Grouping Operators")]
        [Title("Group : Task 8")]
        [Description("This sample groups product by their price in 3 groups")]
        public void Linq008()
        {
            var products = dataSource.Products.GroupBy(p => p.UnitPrice > 50 ? "high price" : p.UnitPrice > 20 ? "medium price" : "low price");

            foreach (var p in products)
            {
                Console.WriteLine(p.Key);
                ObjectDumper.Write(p);
            }
        }

        [Category("Grouping Operators")]
        [Title("Group : Task 9")]
        [Description("This sample return average profitability of city and its intensity")]
        public void Linq009()
        {
            var averageProfitability = dataSource.Customers.GroupBy(c => c.City, (key, c) => new { city = key, average = c.SelectMany(o => o.Orders).Average(p => p.Total) });
            var averageIntensity = dataSource.Customers.GroupBy(c => c.City, (key, c) => new { city = key, average = c.Average(cus => cus.Orders.Count()) });

            Console.WriteLine("profitability:");
            ObjectDumper.Write(averageProfitability);
            Console.WriteLine("intensity:");
            ObjectDumper.Write(averageIntensity);
        }

        [Category("Grouping Operators")]
        [Title("Group : Task 10")]
        [Description("This sample return statistics")]
        public void Linq010()
        {
            var byMonth = dataSource.Customers.SelectMany(c => c.Orders).GroupBy(o => o.OrderDate.Month);
            var byYear = dataSource.Customers.SelectMany(c => c.Orders).GroupBy(o => o.OrderDate.Year);
            var byYearAndMonth = dataSource.Customers.SelectMany(c => c.Orders).GroupBy(o => new { month = o.OrderDate.Month, year = o.OrderDate.Year } );

            Console.WriteLine("by month");
            foreach (var item in byMonth)
            {
                ObjectDumper.Write(item);
            }

            Console.WriteLine("by year");
            foreach (var item in byYear)
            {
                ObjectDumper.Write(item);
            }
            
            Console.WriteLine("by year and month");
            foreach (var item in byYearAndMonth)
            {
                ObjectDumper.Write(item);
            }
        }
    }
}
