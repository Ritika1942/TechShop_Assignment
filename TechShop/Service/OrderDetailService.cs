using System;
using System.Collections.Generic;
using System.Linq;
using TechShop.Model;
using TechShop.Repository;
using YourNamespace.Data;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class OrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IProductRepository productRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
        }

        // Method to retrieve an order detail by ID
        public OrderDetail GetOrderDetailById(int id)
        {
            var orderDetail = _orderDetailRepository.GetOrderDetailById(id);
            if (orderDetail == null)
            {
                throw new KeyNotFoundException($"OrderDetail with ID {id} not found.");
            }
            return orderDetail;
        }

        // Method to add or update an order detail
        public void AddOrUpdateOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                throw new ArgumentNullException(nameof(orderDetail), "OrderDetail cannot be null.");
            }

            if (orderDetail.Id == 0) // Assuming Id is 0 for new records
            {
                _orderDetailRepository.AddOrderDetail(orderDetail);
            }
            else
            {
                _orderDetailRepository.UpdateOrderDetail(orderDetail);
            }
        }

        // Method to remove an order detail by ID
        public void DeleteOrderDetail(int id)
        {
            var orderDetail = _orderDetailRepository.GetOrderDetailById(id);
            if (orderDetail == null)
            {
                throw new KeyNotFoundException($"OrderDetail with ID {id} not found.");
            }
            _orderDetailRepository.DeleteOrderDetail(id);
        }

        // Method to calculate the total inventory value based on product price and quantity
        public decimal GetTotalInventoryValue()
        {
            try
            {
                return _productRepository.GetTotalInventoryValue();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error calculating total inventory value.", ex);
            }
        }

        // Method to get order details for a specific order ID
        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = _orderDetailRepository.GetOrderDetailsByOrderId(orderId);
            if (orderDetails == null || orderDetails.Count == 0)
            {
                throw new KeyNotFoundException($"No OrderDetails found for Order ID {orderId}.");
            }
            return orderDetails;
        }
    }
}
