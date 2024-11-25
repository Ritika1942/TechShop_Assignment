using System;
using TechShop.Model;
using TechShop.Repository;

namespace TechShop.Service
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public decimal CalculateTotalInventoryValue()
        {
            decimal totalInventoryValue = _productRepository.GetTotalInventoryValue();
            Console.WriteLine($"Total Inventory Value: {totalInventoryValue:C}");
            return totalInventoryValue;
        }

        public Products GetProductDetails(int productId)
        {
            Products product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                Console.WriteLine($"Product not found with ID: {productId}");
            }
            else
            {
                Console.WriteLine($"Product Found: ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}, Stock: {product.StockQuantity}");
            }
            return product;
        }

        public void UpdateProductInfo(Products product)
        {
            try
            {
                _productRepository.UpdateProduct(product);
                Console.WriteLine($"Product ID {product.ProductID} updated with new information.");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to update product ID {product.ProductID}. Product not found or update failed.");
            }
        }

        public bool IsProductInStock(int productId)
        {
            Products product = _productRepository.GetProductById(productId);
            if (product != null && product.StockQuantity > 0)
            {
                Console.WriteLine($"Product ID {productId} is in stock.");
                return true;
            }
            else
            {
                Console.WriteLine($"Product ID {productId} is not in stock.");
                return false;
            }
        }
    }
}
