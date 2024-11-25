using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechShop.Model;
using TechShop.Utility;

namespace TechShop.Repository
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;
        private SqlCommand _cmd;

        public CustomerRepository()
        {
            _connectionString = DBConnection.GetConnectionString();
            _cmd = new SqlCommand();
        }

        public void AddCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"INSERT INTO Customers (Name, Email, Phone, Address) 
                                     VALUES (@Name, @Email, @Phone, @Address)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Name", customer.FirstName);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Customer added successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding customer: " + ex.Message);
                }
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Customers 
                                     SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address 
                                     WHERE CustomerID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        cmd.Parameters.AddWithValue("@Name", customer.FirstName);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                        cmd.Parameters.AddWithValue("@Address", customer.Address);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Customer updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating customer: " + ex.Message);
                }
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"DELETE FROM Customers WHERE CustomerID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Customer deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting customer: " + ex.Message);
                }
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Customers WHERE CustomerID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            customer = ExtractCustomer(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving customer by ID: " + ex.Message);
                }
            }
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Customers";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            customers.Add(ExtractCustomer(reader));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving all customers: " + ex.Message);
                }
            }
            return customers;
        }

        public int CalculateTotalOrders(int customerId)
        {
            int totalOrders = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT COUNT(*) FROM Orders WHERE CustomerID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);

                        conn.Open();
                        totalOrders = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error calculating total orders: " + ex.Message);
                }
            }
            return totalOrders;
        }

        public Customer GetCustomerDetails(int customerId)
        {
            return GetCustomerById(customerId); // Assuming details are the same as retrieving by ID
        }

        public void UpdateCustomerInfo(int customerId, string email, string phone, string address)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Customers 
                                     SET Email = @Email, Phone = @Phone, Address = @Address 
                                     WHERE CustomerID = @CustomerID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Address", address);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Customer information updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating customer information: " + ex.Message);
                }
            }
        }

        private Customer ExtractCustomer(SqlDataReader reader)
        {
            return new Customer
            {
                CustomerID = (int)reader["CustomerID"],
                FirstName = (string)reader["Name"],
                Email = (string)reader["Email"],
                Phone = (string)reader["Phone"],
                Address = (string)reader["Address"]
            };
        }
    }
}
