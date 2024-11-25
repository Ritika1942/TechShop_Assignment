using TechShop.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechShop.Utility;

namespace TechShop.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        private SqlCommand _cmd;

        public OrderRepository()
        {
            _connectionString = DBConnection.GetConnectionString();
            _cmd = new SqlCommand();
        }

        public void AddOrder(Orders order)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, Status) 
                                     VALUES (@CustomerID, @OrderDate, @TotalAmount, @Status)";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@CustomerID", order.Customer);
                    _cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    _cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    _cmd.Parameters.AddWithValue("@Status", order.Status);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding order: " + ex.Message);
                }
            }
        }

        public void UpdateOrder(Orders order)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Orders 
                                     SET CustomerID = @CustomerID, OrderDate = @OrderDate, TotalAmount = @TotalAmount, Status = @Status
                                     WHERE OrderID = @OrderID";

                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@OrderID", order.OrderID);
                    _cmd.Parameters.AddWithValue("@CustomerID", order.Customer);
                    _cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    _cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    _cmd.Parameters.AddWithValue("@Status", order.Status);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating order: " + ex.Message);
                }
            }
        }

        public void DeleteOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"DELETE FROM Orders WHERE OrderID = @OrderID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderID", orderId);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting order: " + ex.Message);
                }
            }
        }

        public Orders GetOrderById(int orderId)
        {
            Orders order = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Orders WHERE OrderID = @OrderID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderID", orderId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order = ExtractOrder(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving order by ID: " + ex.Message);
                }
            }

            return order;
        }

        public List<Orders> GetAllOrders()
        {
            List<Orders> orders = new List<Orders>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Orders";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        orders.Add(ExtractOrder(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving all orders: " + ex.Message);
                }
            }

            return orders;
        }

        public decimal CalculateTotalAmount(int orderId)
        {
            decimal totalAmount = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT SUM(Quantity * Price) AS TotalAmount 
                                     FROM OrderDetails WHERE OrderID = @OrderID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderID", orderId);

                    conn.Open();
                    totalAmount = (decimal)_cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error calculating total amount: " + ex.Message);
                }
            }

            return totalAmount;
        }

        public string GetOrderDetails(int orderId)
        {
            string details = string.Empty;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT ProductName, Quantity, Price 
                                     FROM OrderDetails WHERE OrderID = @OrderID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderID", orderId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        details += $"Product: {reader["ProductName"]}, Quantity: {reader["Quantity"]}, Price: {reader["Price"]}\n";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving order details: " + ex.Message);
                }
            }

            return details;
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@Status", status);
                    _cmd.Parameters.AddWithValue("@OrderID", orderId);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating order status: " + ex.Message);
                }
            }
        }

        public void CancelOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Orders SET Status = 'Cancelled' WHERE OrderID = @OrderID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@OrderID", orderId);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error cancelling order: " + ex.Message);
                }
            }
        }

        private Orders ExtractOrder(SqlDataReader reader)
        {
            return new Orders
            {
                OrderID = (int)reader["OrderID"],
                Customer = new Customer { CustomerID = (int)reader["CustomerID"] }, // Assigning only CustomerID for now
                OrderDate = (DateTime)reader["OrderDate"],
                TotalAmount = (decimal)reader["TotalAmount"],
                Status = (string)reader["Status"]
            };
        }

    }
}
