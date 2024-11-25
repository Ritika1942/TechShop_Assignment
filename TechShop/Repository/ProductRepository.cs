using TechShop.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechShop.Utility;

namespace TechShop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        private SqlCommand _cmd;

        public ProductRepository()
        {
            _connectionString = DBConnection.GetConnectionString();
            _cmd = new SqlCommand();
        }

        // Add a new product to the database
        public void AddProduct(Products product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"INSERT INTO Products (ProductName, Description, Price, StockQuantity) 
                                     VALUES (@ProductName, @Description, @Price, @StockQuantity)";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    _cmd.Parameters.AddWithValue("@Description", product.Description);
                    _cmd.Parameters.AddWithValue("@Price", product.Price);
                    _cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding product: " + ex.Message);
                }
            }
        }

        // Update an existing product in the database
        public void UpdateProduct(Products product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Products 
                                     SET ProductName = @ProductName, Description = @Description, Price = @Price, StockQuantity = @StockQuantity
                                     WHERE ProductID = @ProductID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                    _cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    _cmd.Parameters.AddWithValue("@Description", product.Description);
                    _cmd.Parameters.AddWithValue("@Price", product.Price);
                    _cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating product: " + ex.Message);
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"DELETE FROM Products WHERE ProductID = @ProductID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@ProductID", productId);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting product: " + ex.Message);
                }
            }
        }

        public Products GetProductById(int productId)
        {
            Products product = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Products WHERE ProductID = @ProductID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@ProductID", productId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        product = ExtractProduct(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving product by ID: " + ex.Message);
                }
            }

            return product;
        }

        public List<Products> GetAllProducts()
        {
            List<Products> products = new List<Products>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Products";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(ExtractProduct(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving all products: " + ex.Message);
                }
            }

            return products;
        }

        public Products GetProductDetails(int productId)
        {
            Products product = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Products WHERE ProductID = @ProductID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@ProductID", productId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        product = ExtractProduct(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving product details: " + ex.Message);
                }
            }

            return product;
        }

        public void UpdateProductInfo(int productId, string productName, string description, decimal price)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Products SET ProductName = @ProductName, Description = @Description, Price = @Price 
                                     WHERE ProductID = @ProductID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@ProductID", productId);
                    _cmd.Parameters.AddWithValue("@ProductName", productName);
                    _cmd.Parameters.AddWithValue("@Description", description);
                    _cmd.Parameters.AddWithValue("@Price", price);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating product info: " + ex.Message);
                }
            }
        }

        public bool IsProductInStock(int productId)
        {
            bool isInStock = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT StockQuantity FROM Products WHERE ProductID = @ProductID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@ProductID", productId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        isInStock = (int)reader["StockQuantity"] > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error checking stock availability: " + ex.Message);
                }
            }

            return isInStock;
        }

        private Products ExtractProduct(SqlDataReader reader)
        {
            return new Products
            {
                ProductID = (int)reader["ProductID"],
                ProductName = (string)reader["ProductName"],
                Description = (string)reader["Description"],
                Price = (decimal)reader["Price"],
                StockQuantity = (int)reader["StockQuantity"]
            };
        }
    }
}
