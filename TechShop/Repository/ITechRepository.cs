using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Model;

namespace TechShop.Repository
{
    internal interface ITechRepository
    {

        int CalculateTotalOrders(int customerID); 
        Customer GetCustomerDetails(int customerID); 
        bool AddCustomer(Customer customer); 
        bool UpdateCustomer(Customer customer); 
        bool RemoveCustomer(int customerID); 
        Customer GetCustomerById(int customerID);
        List<Customer> SearchCustomers(string keyword); 

        // Product Methods
        Product GetProductDetails(int productID); 
        bool UpdateProduct(Product product);
        bool IsProductInStock(int productID); 
        bool AddProduct(Product product);
        bool RemoveProduct(int productID); 
        Product GetProductById(int productID); 
        List<Product> SearchProducts(string keyword); 

        // Order Methods
        decimal CalculateTotalAmount(int orderID);
        Order GetOrderDetails(int orderID); 
        bool UpdateOrderStatus(int orderID, string status); 
        bool CancelOrder(int orderID);
        bool AddOrder(Order order);
        bool UpdateOrder(Order order); 
        bool RemoveOrder(int orderID);
        Order GetOrderById(int orderID);
        List<Order> GetOrdersByCustomerId(int customerID); 
        // OrderDetails Methods
        decimal CalculateSubtotal(int orderDetailID); 
        OrderDetail GetOrderDetailInfo(int orderDetailID); 
        bool UpdateQuantity(int orderDetailID, int newQuantity);
        bool AddDiscount(int orderDetailID, decimal discountPercentage);

        // Inventory Methods
        Product GetProduct(int inventoryID); 
        int GetQuantityInStock(int productID); 
        bool AddToInventory(int productID, int quantity); 
        bool RemoveFromInventory(int productID, int quantity); 
        bool UpdateStockQuantity(int productID, int newQuantity); 
        bool IsProductAvailable(int productID, int quantityToCheck);
        decimal GetInventoryValue(int inventoryID); 
        List<Inventory> ListLowStockProducts(int threshold); 
        List<Inventory> ListOutOfStockProducts(); 
        List<Inventory> ListAllInventoryItems(); 
        bool IsProductAvailable(object productID, object quantity); 
    }
}
