using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;
using TechShop.Utility;

namespace TechShop.Repository
{
    internal class TechShopRepository : ITechRepository
    {
        private readonly string _connectionString;

        public TechShopRepository()
        {
            _connectionString = DBConnection.GetConnectionString();
        }

        public bool AddCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) 
                                     VALUES (@FirstName, @LastName, @Email, @Phone, @Address)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding customer: {ex.Message}");
                    return false;
                }
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Customers 
                                     SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                                         Phone = @Phone, Address = @Address 
                                     WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating customer: {ex.Message}");
                    return false;
                }
            }
        }

        public bool RemoveCustomer(int customerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error removing customer: {ex.Message}");
                    return false;
                }
            }
        }

        public Customer GetCustomerById(int customerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            Phone = (string)reader["Phone"],
                            Address = (string)reader["Address"]
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching customer by ID: {ex.Message}");
                }
            }
            return null;
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Customers 
                                     WHERE FirstName LIKE @Keyword OR LastName LIKE @Keyword";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            Phone = (string)reader["Phone"],
                            Address = (string)reader["Address"]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error searching customers: {ex.Message}");
                }
            }
            return customers;
        }

        public int CalculateTotalOrders(int customerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Orders WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error calculating total orders: {ex.Message}");
                    return 0;
                }
            }
        }

        public Customer GetCustomerDetails(int customerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Customers WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Email = (string)reader["Email"],
                            Phone = (string)reader["Phone"],
                            Address = (string)reader["Address"]
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching customer details: {ex.Message}");
                }
            }
            return null;
        }

        // --------------- Product Methods ---------------
        public bool AddProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"INSERT INTO Products (ProductName, Description, Price) 
                                     VALUES (@ProductName, @Description, @Price)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding product: {ex.Message}");
                    return false;
                }
            }
        }

        public bool UpdateProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Products 
                                     SET ProductName = @ProductName, Description = @Description, Price = @Price 
                                     WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@ProductID", product.ProductID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating product: {ex.Message}");
                    return false;
                }
            }
        }

        public bool RemoveProduct(int productID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error removing product: {ex.Message}");
                    return false;
                }
            }
        }

        public Product GetProductById(int productID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Products WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = (string)reader["ProductName"],
                            Description = (string)reader["Description"],
                            Price = (decimal)reader["Price"]
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching product by ID: {ex.Message}");
                }
            }
            return null;
        }

        public List<Product> SearchProducts(string keyword)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Products 
                                     WHERE ProductName LIKE @Keyword OR Description LIKE @Keyword";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = (string)reader["ProductName"],
                            Description = (string)reader["Description"],
                            Price = (decimal)reader["Price"]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error searching products: {ex.Message}");
                }
            }
            return products;
        }

        public bool IsProductInStock(int productID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT COUNT(*) FROM Inventory 
                                     WHERE ProductID = @ProductID AND QuantityInStock > 0";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null && Convert.ToInt32(result) > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error checking product stock: {ex.Message}");
                    return false;
                }
            }
        }

        // --------------- Order Methods ---------------
        public bool AddOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) 
                                     VALUES (@CustomerID, @OrderDate, @TotalAmount)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@CustomerID", order.Customer.CustomerID);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding order: {ex.Message}");
                    return false;
                }
            }
        }

        public bool UpdateOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Orders 
                                     SET CustomerID = @CustomerID, OrderDate = @OrderDate, TotalAmount = @TotalAmount 
                                     WHERE OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@CustomerID", order.Customer.CustomerID);
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    cmd.Parameters.AddWithValue("@OrderID", order.OrderID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating order: {ex.Message}");
                    return false;
                }
            }
        }

        public bool RemoveOrder(int orderID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = "DELETE FROM Orders WHERE OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@OrderID", orderID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error removing order: {ex.Message}");
                    return false;
                }
            }
        }

        public Order GetOrderById(int orderID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT o.*, c.FirstName, c.LastName FROM Orders o 
                                     JOIN Customers c ON o.CustomerID = c.CustomerID 
                                     WHERE o.OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@OrderID", orderID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Order
                        {
                            OrderID = (int)reader["OrderID"],
                            Customer = new Customer
                            {
                                CustomerID = (int)reader["CustomerID"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"]
                            },
                            OrderDate = (DateTime)reader["OrderDate"],
                            TotalAmount = (decimal)reader["TotalAmount"]
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching order by ID: {ex.Message}");
                }
            }
            return null;
        }

        public List<Order> GetOrdersByCustomerId(int customerID)
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT o.* FROM Orders o 
                                     WHERE o.CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            OrderID = (int)reader["OrderID"],
                            OrderDate = (DateTime)reader["OrderDate"],
                            TotalAmount = (decimal)reader["TotalAmount"],
                            Customer = new Customer { CustomerID = customerID }
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching orders by customer ID: {ex.Message}");
                }
            }
            return orders;
        }

        // --------------- Inventory Methods ---------------
        public Product GetProduct(int inventoryID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT p.* FROM Inventory i 
                             JOIN Products p ON i.ProductID = p.ProductID 
                             WHERE i.InventoryID = @InventoryID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = (string)reader["ProductName"],
                            Description = (string)reader["Description"],
                            Price = (decimal)reader["Price"]
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching product for inventory ID {inventoryID}: {ex.Message}");
                }
            }
            return null;
        }

        public int GetStockByProductId(int productID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT QuantityInStock FROM Inventory WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting stock for product ID {productID}: {ex.Message}");
                    return 0;
                }
            }
        }

        public bool AddToInventory(int productID, int quantity)
        {
            return UpdateInventory(productID, quantity);
        }


        public bool RemoveFromInventory(int productID, int quantity)
        {
            return UpdateInventory(productID, -quantity);
        }


        public bool UpdateStockQuantity(int productID, int newQuantity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Inventory 
                             SET QuantityInStock = @NewQuantity, LastStockUpdate = GETDATE() 
                             WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);
                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating stock quantity for product ID {productID}: {ex.Message}");
                    return false;
                }
            }
        }


        public bool IsProductAvailable(int productID, int quantityToCheck)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT QuantityInStock FROM Inventory WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    int stock = result != null ? Convert.ToInt32(result) : 0;
                    return stock >= quantityToCheck;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error checking availability for product ID {productID}: {ex.Message}");
                    return false;
                }
            }
        }


        public decimal GetInventoryValue(int inventoryID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT p.Price * i.QuantityInStock AS InventoryValue 
                             FROM Inventory i 
                             JOIN Products p ON i.ProductID = p.ProductID 
                             WHERE i.InventoryID = @InventoryID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error calculating inventory value for inventory ID {inventoryID}: {ex.Message}");
                    return 0;
                }
            }
        }


        public List<Inventory> ListLowStockProducts(int threshold)
        {
            List<Inventory> lowStockProducts = new List<Inventory>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT i.InventoryID, i.ProductID, i.QuantityInStock, p.ProductName 
                             FROM Inventory i 
                             JOIN Products p ON i.ProductID = p.ProductID 
                             WHERE i.QuantityInStock < @Threshold";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Threshold", threshold);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lowStockProducts.Add(new Inventory
                        {
                            InventoryID = (int)reader["InventoryID"],
                            Product = new Product
                            {
                                ProductID = (int)reader["ProductID"],
                                ProductName = (string)reader["ProductName"]
                            },
                            QuantityInStock = (int)reader["QuantityInStock"]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error listing low stock products: {ex.Message}");
                }
            }
            return lowStockProducts;
        }

        public List<Inventory> ListOutOfStockProducts()
        {
            List<Inventory> outOfStockProducts = new List<Inventory>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT i.InventoryID, i.ProductID, p.ProductName 
                             FROM Inventory i 
                             JOIN Products p ON i.ProductID = p.ProductID 
                             WHERE i.QuantityInStock = 0";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        outOfStockProducts.Add(new Inventory
                        {
                            InventoryID = (int)reader["InventoryID"],
                            Product = new Product
                            {
                                ProductID = (int)reader["ProductID"],
                                ProductName = (string)reader["ProductName"]
                            },
                            QuantityInStock = 0
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error listing out-of-stock products: {ex.Message}");
                }
            }
            return outOfStockProducts;
        }


        public List<Inventory> ListAllInventoryItems()
        {
            List<Inventory> inventoryItems = new List<Inventory>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT i.InventoryID, i.ProductID, i.QuantityInStock, p.ProductName 
                             FROM Inventory i 
                             JOIN Products p ON i.ProductID = p.ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        inventoryItems.Add(new Inventory
                        {
                            InventoryID = (int)reader["InventoryID"],
                            Product = new Product
                            {
                                ProductID = (int)reader["ProductID"],
                                ProductName = (string)reader["ProductName"]
                            },
                            QuantityInStock = (int)reader["QuantityInStock"]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error listing all inventory items: {ex.Message}");
                }
            }
            return inventoryItems;
        }

        // --------------- OrderDetails Methods ---------------

        // Calculate the subtotal for an order detail (product quantity * price)
        public decimal CalculateSubtotal(int orderDetailID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT od.Quantity, p.Price 
                             FROM OrderDetails od
                             JOIN Products p ON od.ProductID = p.ProductID
                             WHERE od.OrderDetailID = @OrderDetailID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int quantity = (int)reader["Quantity"];
                        decimal price = (decimal)reader["Price"];
                        return quantity * price;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error calculating subtotal for order detail {orderDetailID}: {ex.Message}");
                }
            }
            return 0;
        }

        // Get detailed information for a specific order detail
        public OrderDetail GetOrderDetailInfo(int orderDetailID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT od.OrderDetailID, od.OrderID, od.ProductID, od.Quantity, p.ProductName 
                             FROM OrderDetails od
                             JOIN Products p ON od.ProductID = p.ProductID
                             WHERE od.OrderDetailID = @OrderDetailID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new OrderDetail
                        {
                            OrderDetailID = (int)reader["OrderDetailID"],
                            OrderID = (int)reader["OrderID"],
                            Product = new Product
                            {
                                ProductID = (int)reader["ProductID"],
                                ProductName = (string)reader["ProductName"]
                            },
                            Quantity = (int)reader["Quantity"]
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching order detail info for {orderDetailID}: {ex.Message}");
                }
            }
            return null;
        }

        // Update the quantity for a specific order detail
        public bool UpdateQuantity(int orderDetailID, int newQuantity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE OrderDetails 
                             SET Quantity = @Quantity 
                             WHERE OrderDetailID = @OrderDetailID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Quantity", newQuantity);
                    cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating quantity for order detail {orderDetailID}: {ex.Message}");
                    return false;
                }
            }
        }

        // Add a discount to the order detail (apply a percentage discount to the price)
        public bool AddDiscount(int orderDetailID, decimal discountPercentage)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    // Get current price of the product
                    string query = @"SELECT p.Price 
                             FROM OrderDetails od
                             JOIN Products p ON od.ProductID = p.ProductID
                             WHERE od.OrderDetailID = @OrderDetailID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);

                    conn.Open();
                    decimal currentPrice = (decimal)cmd.ExecuteScalar();
                    decimal discountedPrice = currentPrice - (currentPrice * (discountPercentage / 100));

                    // Update the price after applying discount
                    query = @"UPDATE OrderDetails 
                      SET DiscountedPrice = @DiscountedPrice 
                      WHERE OrderDetailID = @OrderDetailID";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DiscountedPrice", discountedPrice);
                    cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding discount to order detail {orderDetailID}: {ex.Message}");
                    return false;
                }
            }
        }

        public Product GetProductDetails(int productID)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateTotalAmount(int orderID)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderDetails(int orderID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOrderStatus(int orderID, string status)
        {
            throw new NotImplementedException();
        }

        public bool CancelOrder(int orderID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateInventory(int productID, int quantity)
        {
            throw new NotImplementedException();
        }

        public int GetQuantityInStock(int productID)
        {
            throw new NotImplementedException();
        }

        public bool IsProductAvailable(object productID, object quantity)
        {
            throw new NotImplementedException();
        }
    }
}

