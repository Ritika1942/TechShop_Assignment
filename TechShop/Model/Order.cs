using System;

namespace TechShop.Model
{
    internal class Order
    {
        private int _orderID;
        private Customer _customer;
        private DateTime _orderDate;
        private decimal _totalAmount;

        public int OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (value >= 0)
                    _totalAmount = value;
                else
                    throw new ArgumentException("Total amount cannot be negative.");
            }
        }

        public void CalculateTotalAmount(decimal[] productPrices)
        {
            decimal total = 0;
            foreach (var price in productPrices)
                total += price;

            TotalAmount = total;
            Console.WriteLine($"Total amount for the order: {TotalAmount:C}");
        }

        public string GetOrderDetails()
        {
            return $"Order ID: {OrderID}\n" +
                   $"Customer: {Customer.FirstName} {Customer.LastName}\n" +
                   $"Order Date: {OrderDate}\n" +
                   $"Total Amount: {TotalAmount:C}\n";
        }

        public void UpdateOrderStatus(string status)
        {
            Console.WriteLine($"Order ID {OrderID} status updated to: {status}");
        }

        public void CancelOrder()
        {
            TotalAmount = 0;
            Console.WriteLine($"Order ID {OrderID} has been cancelled. Total amount reset to {TotalAmount:C}.");
        }

        public override string ToString()
        {
            return $"Order ID: {OrderID}, Customer: {Customer.FirstName} {Customer.LastName}, Total: {TotalAmount:C}";
        }
    }
}
