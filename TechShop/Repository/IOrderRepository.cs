using TechShop.Model;
using System.Collections.Generic;

namespace TechShop.Repository
{
    public interface IOrderRepository
    {
        
        void AddOrder(Orders order);
        void UpdateOrder(Orders order);
        void DeleteOrder(int orderId);
        Orders GetOrderById(int orderId);
        List<Orders> GetAllOrders();

        //Task methods
        decimal CalculateTotalAmount(int orderId); 
        string GetOrderDetails(int orderId); 
        void UpdateOrderStatus(int orderId, string status); 
        void CancelOrder(int orderId);
    }
}
