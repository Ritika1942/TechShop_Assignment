using TechShop.Model;

namespace TechShop.Repository
{
    public interface IOrderDetailRepository
    {
        void AddOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(int orderDetailId);
        OrderDetail GetOrderDetailById(int orderDetailId);
        List<OrderDetail> GetAllOrderDetails();

        decimal CalculateSubtotal(int orderDetailId); 
        string GetOrderDetailInfo(int orderDetailId); 
        void UpdateQuantity(int orderDetailId, int quantity); 
        void AddDiscount(int orderDetailId, decimal discountPercentage);
        object GetById(int orderDetailId);
    }
}
