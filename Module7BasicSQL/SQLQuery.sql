--Task 1.1.1
DECLARE @dt DateTime = CONVERT(DateTime,'05/06/1998',101)
SELECT OrderID, ShippedDate, ShipVia
FROM Orders
WHERE ShippedDate >= @dt AND ShipVia >= 2

--Task 1.1.2
SELECT OrderID, ShippedDate =
CASE
	WHEN ShippedDate IS NULL THEN 'Not Shipped'
END
FROM Orders
WHERE ShippedDate IS NULL

--Task 1.1.3
DECLARE @dt3 DateTime = CONVERT(DateTime,'05/06/1998',101)
SELECT OrderID as [Order Number], 'Shipped Date' = 
CASE
	WHEN [ShippedDate] IS NULL THEN 'Not Shipped'
	ELSE CONVERT(VARCHAR, [ShippedDate], 121)
END
FROM Orders
WHERE ShippedDate > @dt3 OR ShippedDate IS NULL

--Task 1.2.1
SELECT ContactName, Country
FROM Customers
WHERE Country IN ('USA', 'Canada')
ORDER BY ContactName, Country

--Task 1.2.2
SELECT ContactName, Country
FROM Customers
WHERE Country NOT IN ('USA', 'Canada')
ORDER BY ContactName

--Task 1.2.3
SELECT DISTINCT Country
FROM Customers
ORDER BY Country DESC

--Task 1.3.1
SELECT DISTINCT OrderID
FROM [Order Details]
WHERE Quantity BETWEEN 3 AND 10

--Task 1.3.2
SELECT CustomerID, Country
FROM Customers
WHERE LEFT(Country, 1) BETWEEN 'b' AND 'g'
ORDER BY Country

--Task 1.3.3
SELECT CustomerID, Country
FROM Customers
WHERE LEFT(Country, 1) LIKE '[b-g]%'
ORDER BY Country

--Task 1.4
SELECT ProductName 
FROM Products
WHERE ProductName LIKE 'cho_olade'

--Task 2.1.1
SELECT SUM(Quantity*UnitPrice*(1 - Discount)) AS Totals
FROM [Order Details]

--Task 2.1.2
SELECT COUNT(*) - COUNT(ALL ShippedDate)
FROM Orders

--Task 2.1.3
SELECT COUNT(DISTINCT CustomerID)
FROM Orders

--Task 2.2.1
SELECT YEAR(OrderDate) AS 'Year', COUNT(*)
FROM Orders
GROUP BY YEAR(OrderDate)

SELECT COUNT(*)
FROM Orders

--Task 2.2.2
SELECT COUNT(*) AS Amount, (SELECT FirstName + LastName FROM Employees WHERE o.EmployeeID = Employees.EmployeeID) AS Seller
FROM Orders AS o
GROUP BY o.EmployeeID
ORDER BY COUNT(*) DESC

--Task 2.2.3
SELECT EmployeeID, CustomerID, COUNT(*) AS Count
FROM Orders
WHERE YEAR(OrderDate) = 1998
GROUP BY EmployeeID, CustomerID