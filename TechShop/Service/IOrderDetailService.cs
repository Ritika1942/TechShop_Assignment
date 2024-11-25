using TechShop.Model;

namespace TechShop.Service
{
    public interface IOrderDetailService
    {
        decimal CalculateSubtotal(int orderDetailId);
        OrderDetail GetOrderDetailInfo(int orderDetailId);
        void UpdateQuantity(int orderDetailId, int quantity);
        void AddDiscount(int orderDetailId, decimal discount);
        decimal CalculateSubtotal(object orderDetailID);
    }
}
