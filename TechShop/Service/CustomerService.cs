using System;
using System.Collections.Generic;
using TechShop.Model;
using TechShop.Repository;

namespace TechShop.Service
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void GetAllCustomers()
        {
            List<Customer> customers = _customerRepository.GetAllCustomers();
            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
            }
            else
            {
                Console.WriteLine("Customers:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"Customer ID: {customer.CustomerID}, Name: {customer.FirstName}, Email: {customer.Email}");
                }
            }
        }

        public void GetCustomerById(int customerId)
        {
            Customer customer = _customerRepository.GetCustomerById(customerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer not found with ID: {customerId}");
            }
            else
            {
                Console.WriteLine($"Customer Found: ID: {customer.CustomerID}, Name: {customer.FirstName}, Email: {customer.Email}, Phone: {customer.Phone}, Address: {customer.Address}");
            }
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
            Console.WriteLine("Customer added successfully.");
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
            Console.WriteLine("Customer updated successfully.");
        }

        public void DeleteCustomer(int customerId)
        {
            _customerRepository.DeleteCustomer(customerId);
            Console.WriteLine("Customer deleted successfully.");
        }

        public void UpdateCustomerInfo(int customerId, string email, string phone, string address)
        {
            _customerRepository.UpdateCustomerInfo(customerId, email, phone, address);
            Console.WriteLine("Customer information updated successfully.");
        }

        // Implementing CalculateTotalOrders to match ICustomerService
        public int CalculateTotalOrders(int customerId)
        {
            int totalOrders = _customerRepository.CalculateTotalOrders(customerId);
            Console.WriteLine($"Total Orders for Customer ID {customerId}: {totalOrders}");
            return totalOrders; // Return value as expected in the interface
        }

        public void GetCustomerDetails(int customerId)
        {
            Customer customer = _customerRepository.GetCustomerDetails(customerId);
            if (customer != null)
            {
                Console.WriteLine($"Customer Details: ID: {customer.CustomerID}, Name: {customer.FirstName}, Email: {customer.Email}, Phone: {customer.Phone}, Address: {customer.Address}");
            }
            else
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
            }
        }

        Customer ICustomerService.GetCustomerDetails(int customerId)
        {
            throw new NotImplementedException();
        }

        void ICustomerService.UpdateCustomerInfo(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
