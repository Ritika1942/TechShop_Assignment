using System;

namespace TechShop.Model
{
    internal class Product
    {
        private int _productID;
        private string _productName;
        private string _description;
        private decimal _price;
        private int _stockQuantity;

        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value >= 0)
                    _price = value;
                else
                    throw new ArgumentException("Price cannot be negative.");
            }
        }

        public int StockQuantity
        {
            get { return _stockQuantity; }
            set
            {
                if (value >= 0)
                    _stockQuantity = value;
                else
                    throw new ArgumentException("Stock quantity cannot be negative.");
            }
        }

        public string GetProductDetails()
        {
            return $"Product ID: {ProductID}\n" +
                   $"Name: {ProductName}\n" +
                   $"Description: {Description}\n" +
                   $"Price: {Price:C}\n" +
                   $"Stock Quantity: {StockQuantity}\n";
        }

        public void UpdateProductInfo(string description, decimal price, int stockQuantity)
        {
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;

            Console.WriteLine("Product information updated successfully!");
        }

        public bool IsProductInStock()
        {
            return StockQuantity > 0;
        }
        public override string ToString()
        {
            return $"Product ID: {ProductID}, Name: {ProductName}, Price: {Price:C}, Stock: {StockQuantity}";
        }
    }
}
