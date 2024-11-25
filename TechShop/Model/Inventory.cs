using System;

namespace TechShop.Model
{
    public class Inventory
    {
        private int _inventoryId;
        private Products? _product; 
        private int _quantityInStock;
        private DateTime _lastStockUpdate;

        public int InventoryID
        {
            get { return _inventoryId; }
            set { _inventoryId = value; }
        }

        public Products? Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set { _quantityInStock = value; }
        }

        public DateTime LastStockUpdate
        {
            get { return _lastStockUpdate; }
            set { _lastStockUpdate = value; }
        }

        public int ProductID { get; internal set; }

        public Inventory() { }
        public Inventory(int inventoryId, Products? product, int quantityInStock, DateTime lastStockUpdate)
        {
            _inventoryId = inventoryId;
            _product = product;
            _quantityInStock = quantityInStock;
            _lastStockUpdate = lastStockUpdate;
        }

        public override string ToString()
        {
            return $"Inventory ID: {InventoryID}, " +
                   $"Product: {Product?.ProductName}, " +
                   $"Quantity In Stock: {QuantityInStock}, " +
                   $"Last Stock Update: {LastStockUpdate.ToShortDateString()}";
        }
    }
}
