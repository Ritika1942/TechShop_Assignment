using System;

namespace TechShop.Model
{
    public class Orders
    {
        private int _orderId;
        private Customer? _customer;  
        private DateTime _orderDate;
        private decimal _totalAmount;
        internal readonly IEnumerable<object> OrderDetails;

        public int OrderID
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public Customer? Customer 
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
            set { _totalAmount = value; }
        }

        public object Status { get; internal set; }
        public string? CustomerName { get; internal set; }

        public Orders() { }

        public Orders(int orderId, Customer? customer, DateTime orderDate, decimal totalAmount)
        {
            _orderId = orderId;
            _customer = customer;
            _orderDate = orderDate;
            _totalAmount = totalAmount;
        }

        public override string ToString()
        {
            return $"Order ID: {OrderID}," +
                $" Customer: {Customer?.FirstName} {Customer?.LastName}, " +
                $"Order Date: {OrderDate.ToShortDateString()}, " +
                $"Total Amount: {TotalAmount:C}";
        }
    }
}
