using TechShop.Model;

namespace TechShop.Service
{
    public interface IProductService
    {
        decimal CalculateTotalInventoryValue();
        Products GetProductDetails(int productId);
        void UpdateProductInfo(Products product);
        bool IsProductInStock(int productId);
    }
}
