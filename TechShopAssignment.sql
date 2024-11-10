CREATE DATABASE TechShopGadget

--task 1 creating tables
--creating customer table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),	
    FirstName VARCHAR(100) NOT NULL,            
    LastName VARCHAR(100) NOT NULL,             
    Email VARCHAR(255) NOT NULL,               
    Phone VARCHAR(15),                          
    Address VARCHAR(255)                       
)

--create products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),   
    ProductName VARCHAR(255) NOT NULL,         
    Description VARCHAR(500),                  
    Price DECIMAL(10, 2) NOT NULL               
)

--creating orders table
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),						
    CustomerID INT NOT NULL,									
    OrderDate DATE NOT NULL,									 
    TotalAmount DECIMAL(10,2) NOT NULL,						 
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)	
)

--creating orderdetails table
CREATE TABLE OrderDetails(
	OrderDetailID INT PRIMARY KEY,							
	OrderID INT,											
	ProductID INT,											
	Quantity INT,											
	FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),		
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID)	
)

--creating inventory table
CREATE TABLE Inventory(
	InventoryID INT PRIMARY KEY,							
	ProductID INT,											
	QuantityInStock INT,									
	LastStockUpdate DATE,									
	FOREIGN KEY (ProductID) REFERENCES Products(ProductID) 
)

--Inserting values
--inserting into customers table
INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) VALUES
('Arshea', 'Mukund', 'arshea@gmail.com', '1234567890', '123 Main St, City A'),
('Jaya', 'Sam', 'jayasam@gmail.com', '0987654321', '456 Oak St, City B'),
('Anu', 'John', 'aanujohn@gmail.com', '1112223333', '789 Pine St, City C'),
('Banu', 'Siva', 'banusiva@gmail.com', '2223334444', '321 Elm St, City D'),
('Chimaya', 'Dina', 'chinmayadina@gmail.com', '3334445555', '654 Cedar St, City E'),
('Deepa', 'Ram', 'deeparam@gmail.com', '4445556666', '987 Birch St, City F'),
('Evana', 'happy', 'evanh@gmail.com', '5556667777', '741 Maple St, City G'),
('Pallavi', 'Salman', 'pallavisal@gmail.com', '6667778888', '852 Walnut St, City H'),
('George', 'Bush', 'georgeb@gmail.com', '7778889999', '963 Spruce St, City I'),
('Harsha', 'Renu', 'harsharenu@gmail.com', '8889990000', '147 Cypress St, City J')

--inserting into products table
INSERT INTO Products (ProductName, Description, Price) VALUES
('Smartphone', 'Latest model with high resolution camera', 699.99),
('Laptop', '14-inch display with 8GB RAM and 256GB SSD', 999.99),
('Tablet', '10-inch display, 64GB storage', 299.99),
('Headphones', 'Wireless headphones with noise cancellation', 149.99),
('Smartwatch', 'Water-resistant smartwatch with heart rate monitor', 199.99),
('Bluetooth Speaker', 'Portable speaker with rich bass', 59.99),
('Gaming Console', 'Latest gaming console with 4K support', 499.99),
('Keyboard', 'Mechanical keyboard with RGB lighting', 89.99),
('Mouse', 'Wireless ergonomic mouse', 29.99),
('Charger', 'Fast charging adapter', 19.99)

--inserting into orderes table
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) VALUES
(1, '2024-01-10', 100.00),
(2, '2024-01-15', 150.50),
(3, '2024-01-20', 300.00),
(4, '2024-01-25', 99.99),
(5, '2024-02-01', 200.00),
(6, '2024-02-05', 120.75),
(7, '2024-02-10', 250.00),
(8, '2024-02-15', 180.00),
(9, '2024-02-20', 145.50),
(10, '2024-02-25', 175.00)

--inserting into order details
INSERT INTO OrderDetails (OrderDetailID, OrderID, ProductID, Quantity) VALUES
(1, 1, 1, 2),
(2, 1, 3, 1),
(3, 2, 4, 1),
(4, 2, 5, 2),
(5, 3, 2, 1),
(6, 3, 6, 1),
(7, 4, 7, 1),
(8, 4, 8, 1),
(9, 5, 9, 3),
(10, 5, 10, 2)

--inseritng into inventory table
INSERT INTO Inventory (InventoryID, ProductID, QuantityInStock, LastStockUpdate) VALUES
(1, 1, 50, '2022-01-15'),
(2, 2, 30, '2023-01-20'),
(3, 3, 40, '2024-02-10'),
(4, 4, 20, '2024-02-25'),
(5, 5, 25, '2022-03-05'),
(6, 6, 10, '2023-03-15'),
(7, 7, 15, '2024-04-01'),
(8, 8, 35, '2024-04-10'),
(9, 9, 45, '2022-04-20'),
(10, 10, 60, '2024-05-01')

--task 2 
--1. Write an SQL query to retrieve the names and emails of all customers

SELECT FirstName, LastName, Email
FROM Customers

--same query using concat to join names
 SELECT CONCAT(FirstName,' ',LastName) as Name,
 Email 
 FROM Customers

--2.Write an SQL query to list all orders with their order dates and corresponding customer names.

SELECT o.OrderID, o.OrderDate, c.FirstName, c.LastName
FROM Orders o, Customers c
WHERE o.CustomerID = c.CustomerID 

/*3.. Write an SQL query to insert a new customer record into the "Customers" table. 
Include customer information such as name, email, and address*/

INSERT INTO Customers (FirstName, LastName, Email, Address)
VALUES ('Janani', 'Sundar', 'jaansundar@gmail.com', '1234 Abv St, Whitefield')

/*4. Write an SQL query to update the prices of all electronic gadgets in the "Products" table by increasing them by 10%.*/
UPDATE Products
SET Price = Price * 1.10

/*5.Write an SQL query to delete a specific order and its associated order details 
from the "Orders" and "OrderDetails" tables. 
Allow users to input the order ID as a parameter*/

ALTER TABLE OrderDetails
ADD CONSTRAINT FK_OrderDetails_Orders
FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
ON DELETE CASCADE
ON UPDATE CASCADE

DELETE FROM Orders
WHERE OrderID = 1

/*6. Write an SQL query to insert a new order into the "Orders" table. 
Include the customer ID, order date, and any other necessary information.*/

INSERT INTO Orders (CustomerID, OrderDate, TotalAmount)
VALUES (1, '2024-11-03', 200.00)

/*7.Write an SQL query to update the contact information (e.g., email and address)
of a specific customer in the "Customers" table. 
Allow users to input the customer ID and new contact information.*/

UPDATE Customers
SET Email = 'banusiva123@gmail.com',  
Address = '456 Dfg St, City M'   
WHERE CustomerID = 4;

SELECT * FROM Customers

/*8.Write an SQL query to recalculate and update the total cost of each order in the "Orders" 
table based on the prices and quantities in the "OrderDetails" table.*/

UPDATE Orders
SET TotalAmount = (
    SELECT SUM(od.Quantity * p.Price)  -- Recalculating the total cost (Quantity * Price)
    FROM OrderDetails od
    JOIN Products p on od.ProductID = p.ProductID  
    WHERE od.OrderID = Orders.OrderID  
)
WHERE EXISTS (
    SELECT 1
    FROM OrderDetails od
    WHERE od.OrderID = Orders.OrderID
)

/*9.Write an SQL query to delete all orders and their associated order details for a specific 
customer from the "Orders" and "OrderDetails" tables. Allow users to input the customer ID as a parameter.*/
DELETE FROM OrderDetails
WHERE OrderID IN (
  SELECT OrderID
  FROM Orders
  WHERE CustomerID = 1
)

DELETE FROM Orders
WHERE CustomerID = 1

/*10. Write an SQL query to insert a new electronic gadget product into the "Products" table,
including product name, category, price, and any other relevant details.*/
ALTER TABLE Products
ADD Category VARCHAR(100)

INSERT INTO Products (ProductName,Description,Category,Price)
VALUES ('Smart Fitness Watch','Water-resistant smartwatch with heart rate monitor, GPS, and notification alerts','Wearables',129.99
)

/*11.Write an SQL query to update the status of a specific order in the "Orders" table 
(e.g., from "Pending" to "Shipped"). Allow users to input the order ID and the new status*/
ALTER TABLE Orders
ADD Status VARCHAR(50)

UPDATE Orders
SET Status = 'Shipped'
WHERE OrderID = 3

SELECt * FROM Orders

/*12. Write an SQL query to calculate and update the number of orders placed by each customer
in the "Customers" table based on the data in the "Orders" table*/

ALTER TABLE Customers
ADD TotalOrders INT;

UPDATE Customers
SET TotalOrders = (SELECT COUNT(OrderID) 
FROM Orders 
WHERE CustomerID = Customers.CustomerID)

SELECT * FROM Customers

--task 3
/*1. Write an SQL query to retrieve a list of all orders along with customer information (e.g., customer name)
for each order.*/

SELECT O.OrderID,CONCAT(C.FirstName,' ',C.LastName) as [Name],C.Email,O.OrderDate,O.TotalAmount
FROM Orders O
JOIN Customers C On O.CustomerID = C.CustomerID

/*2.Write an SQL query to find the total revenue generated by each electronic gadget product. 
Include the product name and the total revenue*/

SELECT P.ProductName,SUM(OD.Quantity * P.Price) AS TotalRevenue
FROM OrderDetails OD
JOIN Products P On OD.ProductID = P.ProductID
GROUP BY P.ProductName 

/*3.Write an SQL query to list all customers who have made at least one purchase. 
Include their names and contact information.*/

SELECT C.CustomerID,C.FirstName,C.LastName,C.Email,C.Phone
FROM Customers C
WHERE C.CustomerID in (SELECT O.CustomerID FROM Orders O)

/*4. Write an SQL query to find the most popular electronic gadget, 
which is the one with the highest total quantity ordered. 
Include the product name and the total quantity ordered*/

SELECT TOP 1 P.ProductName,SUM(OD.Quantity) AS TotalQuantityOrdered
FROM OrderDetails OD
JOIN Products P ON OD.ProductID = P.ProductID
GROUP BY P.ProductName
ORDER BY TotalQuantityOrdered DESC

--5.Write an SQL query to retrieve a list of electronic gadgets along with their corresponding categories
CREATE TABLE Categories (CategoryID INT PRIMARY KEY,CategoryName VARCHAR(255))

INSERT INTO Categories (CategoryID, CategoryName)
VALUES(1, 'Electronics'),(2, 'Gadgets'),(3, 'Computer Accessories'),(4, 'Phone Accessories'),(5, 'Gaming')

ALTER TABLE Products
ADD CategoryID INT

ALTER TABLE Products
ADD CONSTRAINT FK_Products_Categories
FOREIGN KEY (CategoryID)
REFERENCES Categories(CategoryID)

SELECT * FROM Categories
SELECT * FROM	Products

UPDATE Products
SET CategoryID = 1
WHERE ProductName IN ('HeadPhones', 'SmartWatch')

SELECT P.ProductName,C.CategoryName
FROM Products P
JOIN Categories C ON P.CategoryID = C.CategoryID

/*6. Write an SQL query to calculate the average order value for each customer. 
Include the customer's name and their average order value*/
SELECT CONCAT(C.FirstName, ' ', C.LastName) as CustomerName,AVG(O.TotalAmount) as AverageOrderValue
FROM Customers C
JOIN Orders O On C.CustomerID = O.CustomerID
GROUP BY C.FirstName, C.LastName
ORDER BY AverageOrderValue

/*7.Write an SQL query to find the order with the highest total revenue. 
Include the order ID, customer information, and the total revenue.*/
SELECT TOP 1 O.OrderID,
C.FirstName + ' ' + C.LastName AS CustomerName,C.Email,O.TotalAmount as TotalRevenue
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
ORDER BY O.TotalAmount 

ALTER DATABASE [TechShopGadget] MODIFY NAME = [TechShop]
--8.Write an SQL query to list electronic gadgets and the number of times each product has been ordered
SELECT P.ProductName, COUNT(OD.OrderDetailID) AS TimesOrdered
FROM Products P
JOIN OrderDetails OD ON P.ProductID = OD.ProductID
GROUP BY P.ProductName
ORDER BY TimesOrdered DESC

/*9. Write an SQL query to find customers who have purchased a specific electronic gadget product. 
Allow users to input the product name as a parameter.*/


DECLARE @ProductName VARCHAR(255) = 'Smartphone';

SELECT DISTINCT C.CustomerID, C.FirstName, C.LastName, C.Email, C.Phone
FROM Customers C
JOIN Orders O On C.CustomerID = O.CustomerID
JOIN OrderDetails OD On O.OrderID = OD.OrderID
JOIN Products P On OD.ProductID = P.ProductID
WHERE P.ProductName = @ProductName;


/*10.Write an SQL query to calculate the total revenue generated by all orders 
placed within a specific time period. Allow users to input the start and end dates as parameters.*/
DECLARE @StartDate DATE = '2024-01-01'
DECLARE @EndDate DATE = '2024-12-31'  

SELECT SUM(o.TotalAmount) as TotalRevenue  
FROM Orders o
WHERE o.OrderDate between @StartDate and @EndDate 


--task 4 subqueries
--1. Write an SQL query to find out which customers have not placed any orders.
SELECT C.CustomerID, C.FirstName,C.LastName
FROM Customers C
LEFT JOIN Orders O On C.CustomerID = O.CustomerID
WHERE O.CustomerID IS NULL

--2.Write an SQL query to find the total number of products available for sale
SELECT COUNT(*) as TotalProducts
FROM Products

--3.Write an SQL query to calculate the total revenue generated by TechShop.
SELECT SUM(TotalAmount) as TotalRevenue
FROM (SELECT O.OrderID, SUM(OD.Quantity * P.Price) as TotalAmount
FROM Orders O
JOIN OrderDetails OD On O.OrderID = OD.OrderID
JOIN Products P On OD.ProductID = P.ProductID
GROUP BY O.OrderID
) as Revenue

/*4. Write an SQL query to calculate the average quantity ordered for products in a specific category. 
Allow users to input the category name as a parameter*/
DECLARE @CategoryName NVARCHAR(50) = 'Electronics'

SELECT AVG(Quantity) as AverageQtyOrdered
FROM OrderDetails
WHERE ProductID IN (SELECT p.ProductID
FROM Products p
INNER JOIN Categories c on p.CategoryID = c.CategoryID
WHERE c.CategoryName = @CategoryName
)

/*5. Write an SQL query to calculate the total revenue generated by a specific customer. Allow users 
to input the customer ID as a parameter.*/
DECLARE @CustomerID INT = 4
SELECT(
SELECT SUM(od.Quantity * p.Price) 
FROM OrderDetails od
JOIN Orders o on o.OrderID = od.OrderID
JOIN Products p on p.ProductID = od.ProductID
WHERE o.CustomerID = @CustomerID) as TotalRevenue

/*6. Write an SQL query to find the customers who have placed the most orders. List their names 
and the number of orders they've placed*/
SELECT CONCAT(C.FirstName ,' ' ,C.LastName) as CustomerName, 
COUNT(O.OrderID) as NumberOfOrders
FROM Customers C
JOIN Orders O ON C.CustomerID = O.CustomerID
GROUP BY C.FirstName, C.LastName
ORDER BY NumberOfOrders DESC

/*7.Write an SQL query to find the most popular product category, which is the one with the highest 
total quantity ordered across all orders.*/
SELECT TOP 1 c.CategoryName,SUM(od.Quantity) as TotalQuantityOrdered
FROM OrderDetails od
JOIN Products p On od.ProductID = p.ProductID
JOIN Categories c On p.CategoryID = c.CategoryID
GROUP BY c.CategoryName
ORDER BY TotalQuantityOrdered DESC

/*8. Write an SQL query to find the customer who has spent the most money (highest total revenue) 
on electronic gadgets. List their name and total spending*/
SELECT TOP 1 C.FirstName + ' ' + C.LastName as CustomerName, 
SUM(OD.Quantity * P.Price) as TotalSpending
FROM Customers C
JOIN Orders O On C.CustomerID = O.CustomerID
JOIN OrderDetails OD On O.OrderID = OD.OrderID
JOIN Products P On OD.ProductID = P.ProductID
GROUP BY C.FirstName, C.LastName
ORDER BY TotalSpending DESC

/*9. Write an SQL query to calculate the average order value (total revenue divided by the number of 
orders) for all customers*/
SELECT (SELECT SUM(OD.Quantity * P.Price)FROM Orders O
JOIN OrderDetails OD On O.OrderID = OD.OrderID
JOIN Products P On OD.ProductID = P.ProductID)/ 
(SELECT COUNT(DISTINCT OrderID) FROM Orders) as AverageOrderValue --used join

SELECT AVG(TotalAmount) as AverageOrderValue
FROM Orders					--used avg 



/*10. Write an SQL query to find the total number of orders placed by each customer and list their 
names along with the order count*/
SELECT C.FirstName + ' ' + C.LastName as CustomerName, 
(SELECT COUNT(OrderID) 
FROM Orders 
WHERE CustomerID = C.CustomerID) as OrderCount
FROM Customers C
ORDER BY OrderCount DESC








	






 
                









	

   







