using TechShop.Model;

namespace TechShop.Service
{
    public interface IInventoryService
    {
        void AddToInventory(int inventoryId, int quantity);
        void RemoveFromInventory(int inventoryId, int quantity);
        void UpdateStockQuantity(int inventoryId, int newQuantity);
        bool IsProductAvailable(int inventoryId, int quantityToCheck);
        decimal GetInventoryValue();
        List<Inventory> ListLowStockProducts(int threshold);
        List<Inventory> ListOutOfStockProducts();
    }
}
