using TechShop.Model;
using System.Collections.Generic;

namespace TechShop.Repository
{
    public interface IProductRepository
    {
        // CRUD operations for Product
        void AddProduct(Products product);
        void UpdateProduct(Products product);
        void DeleteProduct(int productId);
        Products GetProductById(int productId);
        List<Products> GetAllProducts();

        // Additional methods
        Products GetProductDetails(int productId); 
        void UpdateProductInfo(int productId, string productName, string description, decimal price); 
        bool IsProductInStock(int productId);
        decimal GetTotalInventoryValue();
        object GetById(object productId);
    }
}
