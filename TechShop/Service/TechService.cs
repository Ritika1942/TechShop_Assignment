using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;
using TechShop.Repository;

namespace TechShop.Service
{
  
    internal class TechShopService : ITechService
    {
        private readonly ITechRepository _techShopRepository;

        public TechShopService(ITechRepository techShopRepository)
        {
            _techShopRepository = techShopRepository;
        }

        public int CalculateTotalOrders(int customerID)
        {
            return _techShopRepository.CalculateTotalOrders(customerID);
        }

        public Customer GetCustomerDetails(int customerID)
        {
            return _techShopRepository.GetCustomerDetails(customerID);
        }

        public bool AddCustomer(Customer customer)
        {
            return _techShopRepository.AddCustomer(customer);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return _techShopRepository.UpdateCustomer(customer);
        }

        public bool RemoveCustomer(int customerID)
        {
            return _techShopRepository.RemoveCustomer(customerID);
        }

        public Customer GetCustomerById(int customerID)
        {
            return _techShopRepository.GetCustomerById(customerID);
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            return _techShopRepository.SearchCustomers(keyword);
        }
        public Product GetProductDetails(int productID)
        {
            return _techShopRepository.GetProductDetails(productID);
        }

        public bool UpdateProduct(Product product)
        {
            return _techShopRepository.UpdateProduct(product);
        }

        public bool IsProductInStock(int productID)
        {
            return _techShopRepository.IsProductInStock(productID);
        }

        public bool AddProduct(Product product)
        {
            return _techShopRepository.AddProduct(product);
        }

        public bool RemoveProduct(int productID)
        {
            return _techShopRepository.RemoveProduct(productID);
        }

        public Product GetProductById(int productID)
        {
            return _techShopRepository.GetProductById(productID);
        }

        public List<Product> SearchProducts(string keyword)
        {
            return _techShopRepository.SearchProducts(keyword);
        }

        // Order Methods
        public decimal CalculateTotalAmount(int orderID)
        {
            return _techShopRepository.CalculateTotalAmount(orderID);
        }

        public Order GetOrderDetails(int orderID)
        {
            return _techShopRepository.GetOrderDetails(orderID);
        }

        public bool UpdateOrderStatus(int orderID, string status)
        {
            return _techShopRepository.UpdateOrderStatus(orderID, status);
        }

        public bool CancelOrder(int orderID)
        {
            return _techShopRepository.CancelOrder(orderID);
        }

        public bool AddOrder(Order order)
        {
            return _techShopRepository.AddOrder(order);
        }

        public bool UpdateOrder(Order order)
        {
            return _techShopRepository.UpdateOrder(order);
        }

        public bool RemoveOrder(int orderID)
        {
            return _techShopRepository.RemoveOrder(orderID);
        }

        public Order GetOrderById(int orderID)
        {
            return _techShopRepository.GetOrderById(orderID);
        }

        public List<Order> GetOrdersByCustomerId(int customerID)
        {
            return _techShopRepository.GetOrdersByCustomerId(customerID);
        }

        public decimal CalculateSubtotal(int orderDetailID)
        {
            return _techShopRepository.CalculateSubtotal(orderDetailID);
        }

        public OrderDetail GetOrderDetailInfo(int orderDetailID)
        {
            return _techShopRepository.GetOrderDetailInfo(orderDetailID);
        }

        public bool UpdateQuantity(int orderDetailID, int newQuantity)
        {
            return _techShopRepository.UpdateQuantity(orderDetailID, newQuantity);
        }

        public bool AddDiscount(int orderDetailID, decimal discountPercentage)
        {
            return _techShopRepository.AddDiscount(orderDetailID, discountPercentage);
        }

        public Product GetProduct(int inventoryID)
        {
            return _techShopRepository.GetProduct(inventoryID);
        }

        public int GetQuantityInStock(int productID)
        {
            return _techShopRepository.GetQuantityInStock(productID);
        }

        public bool AddToInventory(int productID, int quantity)
        {
            return _techShopRepository.AddToInventory(productID, quantity);
        }

        public bool RemoveFromInventory(int productID, int quantity)
        {
            return _techShopRepository.RemoveFromInventory(productID, quantity);
        }

        public bool UpdateStockQuantity(int productID, int newQuantity)
        {
            return _techShopRepository.UpdateStockQuantity(productID, newQuantity);
        }

        public bool IsProductAvailable(int productID, int quantityToCheck)
        {
            return _techShopRepository.IsProductAvailable(productID, quantityToCheck);
        }

        public decimal GetInventoryValue(int inventoryID)
        {
            return _techShopRepository.GetInventoryValue(inventoryID);
        }

        public List<Inventory> ListLowStockProducts(int threshold)
        {
            return _techShopRepository.ListLowStockProducts(threshold);
        }

        public List<Inventory> ListOutOfStockProducts()
        {
            return _techShopRepository.ListOutOfStockProducts();
        }

        public List<Inventory> ListAllInventoryItems()
        {
            return _techShopRepository.ListAllInventoryItems();
        }
    }
}

