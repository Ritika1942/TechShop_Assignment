using System;
using System.Collections.Generic;
using TechShop.Service; 
using TechShop.Models; 
using TechShop.Repository; 

namespace TechShop.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                var productService = new ProductService();
                var orderService = new OrderService();
                var productCatalog = productService.GetAllProducts();
                Dictionary<int, OrderDetail> orderDetails = new Dictionary<int, OrderDetail>();

                Console.WriteLine("Welcome to TechShop!");
                bool isRunning = true;

                while (isRunning)
                {
                    Console.WriteLine("\nSelect an option:");
                    Console.WriteLine("1. View all products");
                    Console.WriteLine("2. Search product by name");
                    Console.WriteLine("3. Create a new order");
                    Console.WriteLine("4. View order details");
                    Console.WriteLine("5. Modify order status");
                    Console.WriteLine("6. Add product to order");
                    Console.WriteLine("7. Remove product from order");
                    Console.WriteLine("8. Exit");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ViewAllProducts(productCatalog);
                            break;

                        case "2":
                            SearchProductByName(productCatalog);
                            break;

                        case "3":
                            CreateOrder(orderService);
                            break;

                        case "4":
                            ViewOrderDetails(orderService);
                            break;

                        case "5":
                            ModifyOrderStatus(orderService);
                            break;

                        case "6":
                            AddProductToOrder(productCatalog, orderService, orderDetails);
                            break;

                        case "7":
                            RemoveProductFromOrder(orderDetails);
                            break;

                        case "8":
                            isRunning = false;
                            Console.WriteLine("Exiting application. Thank you for using TechShop!");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // View all products
        static void ViewAllProducts(List<Product> products)
        {
            Console.WriteLine("\nProduct Catalog:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}");
            }
        }

        // Search product by name
        static void SearchProductByName(List<Product> products)
        {
            Console.WriteLine("Enter the product name to search:");
            string productName = Console.ReadLine();
            var foundProducts = products.FindAll(p => p.Name.ToLower().Contains(productName.ToLower()));

            if (foundProducts.Count > 0)
            {
                Console.WriteLine("Found the following products:");
                foreach (var product in foundProducts)
                {
                    Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}");
                }
            }
            else
            {
                Console.WriteLine("No products found matching the given name.");
            }
        }

        // Create a new order
        static void CreateOrder(OrderService orderService)
        {
            Order order = new Order();
            Console.WriteLine("Enter Order ID:");
            string orderIdInput = Console.ReadLine();
            if (int.TryParse(orderIdInput, out int orderId))
            {
                order.OrderId = orderId;
            }
            else
            {
                Console.WriteLine("Invalid Order ID input. Please enter a valid number.");
                return;
            }

            Console.WriteLine("Enter Order Status (e.g., Pending, Completed):");
            string status = Console.ReadLine();
            order.Status = status ?? "Pending";  

            Console.WriteLine($"Order {order.OrderId} created with status: {order.Status}");
            orderService.CreateOrder(order);
        }

        // View the details of an order
        static void ViewOrderDetails(OrderService orderService)
        {
            Console.WriteLine("Enter Order ID to view details:");
            string orderIdInput = Console.ReadLine();
            if (int.TryParse(orderIdInput, out int orderId))
            {
                var order = orderService.GetOrderById(orderId);
                if (order != null)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Status: {order.Status}");
                    Console.WriteLine("Order Details:");
                    foreach (var detail in order.OrderDetails)
                    {
                        Console.WriteLine($"Product ID: {detail.ProductId}, Quantity: {detail.Quantity}, Price: {detail.Price}");
                    }
                    Console.WriteLine($"Total Order Price: {order.TotalPrice}");
                }
                else
                {
                    Console.WriteLine("Order not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Order ID input.");
            }
        }

        // Modify order status
        static void ModifyOrderStatus(OrderService orderService)
        {
            Console.WriteLine("Enter Order ID to modify:");
            string orderIdInput = Console.ReadLine();
            if (int.TryParse(orderIdInput, out int orderId))
            {
                var order = orderService.GetOrderById(orderId);
                if (order != null)
                {
                    Console.WriteLine("Enter new status for the order (e.g., Pending, Completed):");
                    string newStatus = Console.ReadLine();
                    order.Status = newStatus ?? order.Status;
                    orderService.UpdateOrderStatus(order);
                    Console.WriteLine($"Order {order.OrderId} status updated to {order.Status}.");
                }
                else
                {
                    Console.WriteLine("Order not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Order ID input.");
            }
        }

        // Add a product to an order
        static void AddProductToOrder(List<Product> products, OrderService orderService, Dictionary<int, OrderDetail> orderDetails)
        {
            Console.WriteLine("Enter Order ID to add product:");
            string orderIdInput = Console.ReadLine();
            if (int.TryParse(orderIdInput, out int orderId))
            {
                Console.WriteLine("Available Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}");
                }

                Console.WriteLine("Enter Product ID to add to order:");
                string productIdInput = Console.ReadLine();
                if (int.TryParse(productIdInput, out int productId))
                {
                    Console.WriteLine("Enter Quantity:");
                    string quantityInput = Console.ReadLine();
                    if (int.TryParse(quantityInput, out int quantity))
                    {
                        var product = products.Find(p => p.ProductId == productId);
                        if (product != null)
                        {
                            OrderDetail orderDetail = new OrderDetail
                            {
                                ProductId = productId,
                                Quantity = quantity,
                                Price = product.Price
                            };
                            orderDetails[productId] = orderDetail;
                            Console.WriteLine($"Product {product.Name} added to order {orderId}.");
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Product ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Order ID.");
            }
        }

        // Remove product from an order
        static void RemoveProductFromOrder(Dictionary<int, OrderDetail> orderDetails)
        {
            Console.WriteLine("Enter Order ID to remove product:");
            string orderIdInput = Console.ReadLine();
            if (int.TryParse(orderIdInput, out int orderId))
            {
                Console.WriteLine("Enter Product ID to remove from order:");
                string productIdInput = Console.ReadLine();
                if (int.TryParse(productIdInput, out int productId))
                {
                    if (orderDetails.ContainsKey(productId))
                    {
                        orderDetails.Remove(productId);
                        Console.WriteLine($"Product {productId} removed from order {orderId}.");
                    }
                    else
                    {
                        Console.WriteLine("Product not found in order.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Product ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Order ID.");
            }
        }
    }
}
