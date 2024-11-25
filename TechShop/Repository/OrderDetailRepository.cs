using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechShop.Model;
using TechShop.Utility;

namespace TechShop.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly string _connectionString;
        private readonly SqlCommand _cmd;

        public OrderDetailRepository()
        {
            _connectionString = DBConnection.GetConnectionString(); // Ensure the connection string is correctly set
            _cmd = new SqlCommand();
        }

        // Add a new order detail
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price, Discount) 
                                     VALUES (@OrderID, @ProductID, @Quantity, @Price, @Discount)";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
                    _cmd.Parameters.AddWithValue("@ProductID", orderDetail.Product.ProductID); // Assuming `Product` is an object
                    _cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    _cmd.Parameters.AddWithValue("@Price", orderDetail.Price);
                    _cmd.Parameters.AddWithValue("@Discount", orderDetail.Discount);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding order detail: " + ex.Message);
                }
            }
        }

        // Update an existing order detail
        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE OrderDetails 
                                     SET Quantity = @Quantity, Price = @Price, Discount = @Discount
                                     WHERE OrderDetailID = @OrderDetailID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderDetailID", orderDetail.OrderDetailID);
                    _cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    _cmd.Parameters.AddWithValue("@Price", orderDetail.Price);
                    _cmd.Parameters.AddWithValue("@Discount", orderDetail.Discount);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating order detail: " + ex.Message);
                }
            }
        }

        // Delete an order detail by ID
        public void DeleteOrderDetail(int orderDetailId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"DELETE FROM OrderDetails WHERE OrderDetailID = @OrderDetailID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailId);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting order detail: " + ex.Message);
                }
            }
        }

        // Get an order detail by ID
        public OrderDetail GetOrderDetailById(int orderDetailId)
        {
            OrderDetail orderDetail = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT od.OrderDetailID, od.OrderID, od.ProductID, od.Quantity, od.Price, od.Discount, p.ProductName 
                                     FROM OrderDetails od
                                     INNER JOIN Products p ON od.ProductID = p.ProductID
                                     WHERE od.OrderDetailID = @OrderDetailID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        orderDetail = ExtractOrderDetail(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching order detail: " + ex.Message);
                }
            }

            return orderDetail;
        }

        // Get all order details
        public List<OrderDetail> GetAllOrderDetails()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT od.OrderDetailID, od.OrderID, od.ProductID, od.Quantity, od.Price, od.Discount, p.ProductName 
                                     FROM OrderDetails od
                                     INNER JOIN Products p ON od.ProductID = p.ProductID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        orderDetails.Add(ExtractOrderDetail(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching all order details: " + ex.Message);
                }
            }

            return orderDetails;
        }

        // Calculate the subtotal for an order detail
        public decimal CalculateSubtotal(int orderDetailId)
        {
            decimal subtotal = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT Quantity, Price, Discount 
                                     FROM OrderDetails 
                                     WHERE OrderDetailID = @OrderDetailID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int quantity = (int)reader["Quantity"];
                        decimal price = (decimal)reader["Price"];
                        decimal discount = (decimal)reader["Discount"];
                        subtotal = quantity * price * (1 - discount / 100);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error calculating subtotal: " + ex.Message);
                }
            }

            return subtotal;
        }

        // Get detailed information for an order detail
        public string GetOrderDetailInfo(int orderDetailId)
        {
            string orderDetailInfo = string.Empty;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT od.OrderDetailID, od.Quantity, od.Price, od.Discount, p.ProductName 
                                     FROM OrderDetails od
                                     INNER JOIN Products p ON od.ProductID = p.ProductID
                                     WHERE od.OrderDetailID = @OrderDetailID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        orderDetailInfo = $"Product: {reader["ProductName"]}, " +
                                          $"Quantity: {reader["Quantity"]}, " +
                                          $"Price: {reader["Price"]}, " +
                                          $"Discount: {reader["Discount"]}%";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error getting order detail info: " + ex.Message);
                }
            }

            return orderDetailInfo;
        }

        // Update the quantity for an order detail
        public void UpdateQuantity(int orderDetailId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE OrderDetails 
                                     SET Quantity = @Quantity
                                     WHERE OrderDetailID = @OrderDetailID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailId);
                    _cmd.Parameters.AddWithValue("@Quantity", quantity);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating quantity: " + ex.Message);
                }
            }
        }

        // Add discount to an order detail
        public void AddDiscount(int orderDetailId, decimal discountPercentage)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE OrderDetails 
                                     SET Discount = @Discount
                                     WHERE OrderDetailID = @OrderDetailID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailId);
                    _cmd.Parameters.AddWithValue("@Discount", discountPercentage);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding discount: " + ex.Message);
                }
            }
        }

        // Helper method to extract OrderDetail data from SqlDataReader
        private OrderDetail ExtractOrderDetail(SqlDataReader reader)
        {
            Products product = new Products
            {
                ProductID = (int)reader["ProductID"],
                ProductName = reader["ProductName"].ToString(),
                Price = (decimal)reader["Price"]
            };

            return new OrderDetail
            {
                OrderDetailID = (int)reader["OrderDetailID"],
                OrderID = (int)reader["OrderID"],
                Product = product,
                Quantity = (int)reader["Quantity"],
                Price = (decimal)reader["Price"],
                Discount = (decimal)reader["Discount"]
            };
        }
    }
}
