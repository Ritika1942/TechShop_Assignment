using System;

namespace TechShop.Model
{
    internal class Customer
    {
        private int _customerID;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _address;
        private int _totalOrders;

        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value.Contains("@"))
                    _email = value;
                else
                    throw new ArgumentException("Invalid email format.");
            }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public int TotalOrders
        {
            get { return _totalOrders; }
            set { _totalOrders = value; }
        }

        public int CalculateTotalOrders()
        {
            Console.WriteLine($"Total orders placed by {FirstName} {LastName}: {TotalOrders}");
            return TotalOrders;
        }

        public string GetCustomerDetails()
        {
            return $"Customer ID: {CustomerID}\n" +
                   $"Name: {FirstName} {LastName}\n" +
                   $"Email: {Email}\n" +
                   $"Phone: {Phone}\n" +
                   $"Address: {Address}\n" +
                   $"Total Orders: {TotalOrders}\n";
        }

        public void UpdateCustomerInfo(string email, string phone, string address)
        {
            Email = email;
            Phone = phone;
            Address = address;

            Console.WriteLine("Customer information updated successfully!");
        }

        public override string ToString()
        {
            return $"Customer ID: {CustomerID}, Name: {FirstName} {LastName}, Email: {Email}, Phone: {Phone}, Address: {Address}, Total Orders: {TotalOrders}";
        }
    }
}
