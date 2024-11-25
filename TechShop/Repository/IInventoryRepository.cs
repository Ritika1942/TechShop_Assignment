using TechShop.Model;

namespace TechShop.Repository
{
    public interface IInventoryRepository
    {
     
        void AddInventoryItem(Inventory inventoryItem);
        void UpdateInventoryItem(Inventory inventoryItem);
        void RemoveInventoryItem(int inventoryId);
        Inventory GetInventoryItemById(int inventoryId);
        List<Inventory> GetAllInventoryItems();

        //task methods
        Products GetProduct(int inventoryId);
        int GetQuantityInStock(int inventoryId); 
        void AddToInventory(int inventoryId, int quantity); 
        void RemoveFromInventory(int inventoryId, int quantity); 
        void UpdateStockQuantity(int inventoryId, int newQuantity); 
        bool IsProductAvailable(int inventoryId, int quantityToCheck); 
        decimal GetInventoryValue();
        List<Inventory> ListLowStockProducts(int threshold); 
        List<Inventory> ListOutOfStockProducts();
        IEnumerable<object> GetAll();
        object GetById(int inventoryId);
    }
}
