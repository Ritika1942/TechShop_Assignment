using TechShop.Model;
using System.Collections.Generic;

namespace TechShop.Repository
{
    public interface ICustomerRepository
    {
        // CRUD operations for Customer
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        Customer GetCustomerById(int customerId);
        List<Customer> GetAllCustomers();
        //task methods
        int CalculateTotalOrders(int customerId); 
        Customer GetCustomerDetails(int customerId); 
        void UpdateCustomerInfo(int customerId, string email, string phone, string address); 
    }
}
