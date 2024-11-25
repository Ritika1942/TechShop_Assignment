using TechShop.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechShop.Utility;

namespace TechShop.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly string _connectionString;
        private SqlCommand _cmd;

        public InventoryRepository()
        {
            _connectionString = DBConnection.GetConnectionString();
            _cmd = new SqlCommand();
        }

        public void AddInventoryItem(Inventory inventoryItem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"INSERT INTO Inventory (ProductID, QuantityInStock) 
                                     VALUES (@ProductID, @QuantityInStock)";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@ProductID", inventoryItem.ProductID);
                    _cmd.Parameters.AddWithValue("@QuantityInStock", inventoryItem.QuantityInStock);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding inventory item: " + ex.Message);
                }
            }
        }

        public void UpdateInventoryItem(Inventory inventoryItem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Inventory 
                                     SET QuantityInStock = @QuantityInStock 
                                     WHERE InventoryID = @InventoryID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@InventoryID", inventoryItem.InventoryID);
                    _cmd.Parameters.AddWithValue("@QuantityInStock", inventoryItem.QuantityInStock);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating inventory item: " + ex.Message);
                }
            }
        }

        public void RemoveInventoryItem(int inventoryId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"DELETE FROM Inventory WHERE InventoryID = @InventoryID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@InventoryID", inventoryId);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error removing inventory item: " + ex.Message);
                }
            }
        }

        public Inventory GetInventoryItemById(int inventoryId)
        {
            Inventory inventoryItem = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Inventory WHERE InventoryID = @InventoryID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@InventoryID", inventoryId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        inventoryItem = ExtractInventoryItem(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving inventory item: " + ex.Message);
                }
            }

            return inventoryItem;
        }

        public List<Inventory> GetAllInventoryItems()
        {
            List<Inventory> inventoryItems = new List<Inventory>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Inventory";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        inventoryItems.Add(ExtractInventoryItem(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving all inventory items: " + ex.Message);
                }
            }

            return inventoryItems;
        }

        public Products GetProduct(int inventoryId)
        {
            Products product = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT p.* 
                                     FROM Products p
                                     INNER JOIN Inventory i ON p.ProductID = i.ProductID
                                     WHERE i.InventoryID = @InventoryID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@InventoryID", inventoryId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        product = ExtractProduct(reader);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving product: " + ex.Message);
                }
            }

            return product;
        }

        public int GetQuantityInStock(int inventoryId)
        {
            int quantityInStock = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT QuantityInStock FROM Inventory WHERE InventoryID = @InventoryID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@InventoryID", inventoryId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        quantityInStock = (int)reader["QuantityInStock"];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving quantity in stock: " + ex.Message);
                }
            }

            return quantityInStock;
        }

        public void AddToInventory(int inventoryId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Inventory SET QuantityInStock = QuantityInStock + @Quantity 
                                     WHERE InventoryID = @InventoryID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                    _cmd.Parameters.AddWithValue("@Quantity", quantity);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding to inventory: " + ex.Message);
                }
            }
        }

        public void RemoveFromInventory(int inventoryId, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Inventory SET QuantityInStock = QuantityInStock - @Quantity 
                                     WHERE InventoryID = @InventoryID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                    _cmd.Parameters.AddWithValue("@Quantity", quantity);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error removing from inventory: " + ex.Message);
                }
            }
        }

        public void UpdateStockQuantity(int inventoryId, int newQuantity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Inventory SET QuantityInStock = @NewQuantity 
                                     WHERE InventoryID = @InventoryID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();

                    _cmd.Parameters.AddWithValue("@InventoryID", inventoryId);
                    _cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);

                    conn.Open();
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating stock quantity: " + ex.Message);
                }
            }
        }

        public bool IsProductAvailable(int inventoryId, int quantityToCheck)
        {
            bool isAvailable = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT QuantityInStock FROM Inventory WHERE InventoryID = @InventoryID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@InventoryID", inventoryId);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int availableQuantity = (int)reader["QuantityInStock"];
                        isAvailable = availableQuantity >= quantityToCheck;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error checking product availability: " + ex.Message);
                }
            }

            return isAvailable;
        }

        public decimal GetInventoryValue()
        {
            decimal totalValue = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT SUM(i.QuantityInStock * p.Price) AS TotalValue
                                     FROM Inventory i
                                     JOIN Products p ON i.ProductID = p.ProductID";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        totalValue = reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error calculating inventory value: " + ex.Message);
                }
            }

            return totalValue;
        }
        public List<Inventory> ListLowStockProducts(int threshold)
        {
            List<Inventory> lowStockProducts = new List<Inventory>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Inventory WHERE QuantityInStock <= @Threshold";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@Threshold", threshold);

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lowStockProducts.Add(ExtractInventoryItem(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error listing low stock products: " + ex.Message);
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
                    string query = @"SELECT * FROM Inventory WHERE QuantityInStock = 0";
                    _cmd.CommandText = query;
                    _cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader reader = _cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        outOfStockProducts.Add(ExtractInventoryItem(reader));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error listing out of stock products: " + ex.Message);
                }
            }

            return outOfStockProducts;
        }
        private Inventory ExtractInventoryItem(SqlDataReader reader)
        {
            return new Inventory
            {
                InventoryID = (int)reader["InventoryID"],
                ProductID = (int)reader["ProductID"],
                QuantityInStock = (int)reader["QuantityInStock"]
            };
        }

        private Products ExtractProduct(SqlDataReader reader)
        {
            return new Products
            {
                ProductID = (int)reader["ProductID"],
                ProductName = reader["ProductName"].ToString(),
                Price = (decimal)reader["Price"]
            };
        }
    }
}
