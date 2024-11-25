using TechShop.Model;

namespace TechShop.Service
{
    public interface ICustomerService
    {
        int CalculateTotalOrders(int customerId);
        Customer GetCustomerDetails(int customerId);
        void UpdateCustomerInfo(Customer customer);
    }
}
