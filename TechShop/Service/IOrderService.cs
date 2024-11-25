using TechShop.Model;

namespace TechShop.Service
{
    public interface IOrderService
    {
        decimal CalculateTotalAmount(int orderId);
        Orders GetOrderDetails(int orderId);
        void UpdateOrderStatus(int orderId, string status);
        void CancelOrder(int orderId);
    }
}
