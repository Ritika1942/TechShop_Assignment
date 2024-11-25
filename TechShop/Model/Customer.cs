using System;

namespace TechShop.Model
{
    public class Customer
    {
        private int _customerId;
        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private string? _phone;
        private string? _address;

        // Properties
        public int CustomerID
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public string? FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string? LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string? Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string? Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string? Address
        {
            get { return _address; }
            set { _address = value; }
        }

        // Default Constructor
        public Customer() { }

        // Parameterized Constructor
        public Customer(int customerId, string? firstName, string? lastName, string? email, string? phone, string? address)
        {
            _customerId = customerId;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phone = phone;
            _address = address;
        }

        public override string ToString()
        {
            return $"Customer ID: {CustomerID}," +
                $" Name: {FirstName} {LastName}, " +
                $"Email: {Email}, " +
                $"Phone: {Phone}, " +
                $"Address: {Address}";
        }
    }
}
