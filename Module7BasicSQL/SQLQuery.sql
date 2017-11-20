--Task 1
DECLARE @dt DateTime = CONVERT(DateTime,'05/01/1998',101)
SELECT OrderID, ShippedDate, ShipVia
FROM Orders
WHERE ShippedDate > @dt AND ShipVia >= 2

--Task 2
SELECT OrderID, ShippedDate =
CASE
	WHEN ShippedDate IS NULL THEN 'Not Shipped'
END
FROM Orders
WHERE ShippedDate IS NULL

--Task 3