using System;
using System.Collections.Generic;
using System.Linq;
using TechShop.Model;
using TechShop.Repository;

namespace TechShop.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
        }

        // Add quantity to the inventory
        public void AddToInventory(int inventoryId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity to add must be greater than zero.", nameof(quantity));
            }

            var inventoryItem = _inventoryRepository.GetById(inventoryId);
            if (inventoryItem != null)
            {
                inventoryItem.Quantity += quantity;
                _inventoryRepository.Update(inventoryItem);
            }
            else
            {
                throw new KeyNotFoundException($"Inventory item with ID {inventoryId} not found.");
            }
        }

        // Remove quantity from the inventory
        public void RemoveFromInventory(int inventoryId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity to remove must be greater than zero.", nameof(quantity));
            }

            var inventoryItem = _inventoryRepository.GetById(inventoryId);
            if (inventoryItem != null)
            {
                if (inventoryItem.Quantity < quantity)
                {
                    throw new InvalidOperationException("Not enough stock to remove.");
                }

                inventoryItem.Quantity -= quantity;
                _inventoryRepository.UpdateInventoryItem(inventoryItem);
            }
            else
            {
                throw new KeyNotFoundException($"Inventory item with ID {inventoryId} not found.");
            }
        }

        // Update the stock quantity in the inventory
        public void UpdateStockQuantity(int inventoryId, int newQuantity)
        {
            if (newQuantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot be negative.", nameof(newQuantity));
            }

            var inventoryItem = _inventoryRepository.GetById(inventoryId);
            if (inventoryItem != null)
            {
                inventoryItem.Quantity = newQuantity;
                _inventoryRepository.Update(inventoryItem);
            }
            else
            {
                throw new KeyNotFoundException($"Inventory item with ID {inventoryId} not found.");
            }
        }

        // Check if the product is available in the inventory
        public bool IsProductAvailable(int inventoryId, int quantityToCheck)
        {
            if (quantityToCheck <= 0)
            {
                throw new ArgumentException("Quantity to check must be greater than zero.", nameof(quantityToCheck));
            }

            var inventoryItem = _inventoryRepository.GetById(inventoryId);
            return inventoryItem != null && inventoryItem.Quantity >= quantityToCheck;
        }

        // Get the total value of the inventory
        public decimal GetInventoryValue()
        {
            var inventoryItems = _inventoryRepository.GetAll();
            return inventoryItems.Sum(item => item.Quantity * item.Price);
        }

        // List products with low stock (below a threshold)
        public List<Inventory> ListLowStockProducts(int threshold)
        {
            if (threshold <= 0)
            {
                throw new ArgumentException("Threshold must be greater than zero.", nameof(threshold));
            }

            return _inventoryRepository.GetAll()
                .Where(item => item.Quantity < threshold)
                .ToList();
        }

        // List products that are out of stock (quantity = 0)
        public List<Inventory> ListOutOfStockProducts()
        {
            return _inventoryRepository.GetAll()
                .Where(item => item.Quantity == 0)
                .ToList();
        }
    }
}
