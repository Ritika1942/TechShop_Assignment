using System;

namespace TechShop.Model
{
    internal class Inventory
    {
        private int _inventoryID;
        private Product _product; 
        private int _quantityInStock;
        private DateTime _lastStockUpdate;

        public int InventoryID
        {
            get { return _inventoryID; }
            set { _inventoryID = value; }
        }

        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set
            {
                if (value >= 0)
                    _quantityInStock = value;
                else
                    throw new ArgumentException("Stock quantity cannot be negative.");
            }
        }

        public DateTime LastStockUpdate
        {
            get { return _lastStockUpdate; }
            set { _lastStockUpdate = value; }
        }

        public Product GetProduct()
        {
            return Product; 
        }

        public int GetQuantityInStock()
        {
            return QuantityInStock;
        }
        public void AddToInventory(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity to add must be greater than zero.");

            QuantityInStock += quantity;
            LastStockUpdate = DateTime.Now;

            Console.WriteLine($"{quantity} units added to inventory. Total stock for {Product.ProductName}: {QuantityInStock}");
        }

        public void RemoveFromInventory(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity to remove must be greater than zero.");

            if (quantity > QuantityInStock)
                throw new InvalidOperationException($"Insufficient stock. Only {QuantityInStock} units available.");

            QuantityInStock -= quantity;
            LastStockUpdate = DateTime.Now;

            Console.WriteLine($"{quantity} units removed from inventory. Remaining stock for {Product.ProductName}: {QuantityInStock}");
        }

        public void UpdateStockQuantity(int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("New stock quantity cannot be negative.");

            QuantityInStock = newQuantity;
            LastStockUpdate = DateTime.Now;

            Console.WriteLine($"Stock updated. New quantity for {Product.ProductName}: {QuantityInStock}");
        }

        public bool IsProductAvailable(int quantityToCheck)
        {
            return QuantityInStock >= quantityToCheck;
        }

        public decimal GetInventoryValue()
        {
            return Product.Price * QuantityInStock;
        }

        public bool IsLowStock(int threshold)
        {
            return QuantityInStock < threshold;
        }

        public void ListLowStockProducts(int threshold)
        {
            if (IsLowStock(threshold))
            {
                Console.WriteLine($"Low stock alert for {Product.ProductName}: {QuantityInStock} units (threshold: {threshold}).");
            }
        }

        public bool IsOutOfStock()
        {
            return QuantityInStock == 0;
        }

        public void ListOutOfStockProducts()
        {
            if (IsOutOfStock())
            {
                Console.WriteLine($"Out of stock: {Product.ProductName}");
            }
        }

        public void ListAllProducts()
        {
            Console.WriteLine($"Inventory ID: {InventoryID}\n" +
                              $"Product: {Product.ProductName}\n" +
                              $"Quantity in Stock: {QuantityInStock}\n" +
                              $"Last Stock Update: {LastStockUpdate}\n");
        }

        public override string ToString()
        {
            return $"Inventory ID: {InventoryID}, Product: {Product.ProductName}, Stock: {QuantityInStock}, Last Updated: {LastStockUpdate}";
        }
    }
}
