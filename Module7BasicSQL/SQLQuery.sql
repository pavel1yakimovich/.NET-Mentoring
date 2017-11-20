--Task 1
DECLARE @dt DateTime = CONVERT(DateTime,'05/06/1998',101)
SELECT OrderID, ShippedDate, ShipVia
FROM Orders
WHERE ShippedDate >= @dt AND ShipVia >= 2

--Task 2
SELECT OrderID, ShippedDate =
CASE
	WHEN ShippedDate IS NULL THEN 'Not Shipped'
END
FROM Orders
WHERE ShippedDate IS NULL

--Task 3
DECLARE @dt3 DateTime = CONVERT(DateTime,'05/06/1998',101)
SELECT OrderID as [Order Number], 'Shipped Date' = 
CASE
	WHEN [ShippedDate] IS NULL THEN 'Not Shipped'
	ELSE CONVERT(VARCHAR, [ShippedDate], 121)
END
FROM Orders
WHERE ShippedDate > @dt3 OR ShippedDate IS NULL