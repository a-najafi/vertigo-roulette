using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace VertigoRouletteMiniGame.ApplicationFlow.Inventory
{
    /// <summary>
    /// Represents a single inventory item, with unique ID and quantity.
    /// </summary>
    [Serializable]
    public class PlayerInventoryItem
    {
        #region Serialized Parameters

        [SerializeField] private string _itemUniqueId = "Invalid";
        [SerializeField] private int _count = 0;

        #endregion

        #region Properties

        /// <summary>
        /// The unique identifier for this item.
        /// </summary>
        public string ItemUniqueId => _itemUniqueId;

        /// <summary>
        /// The count/amount of this item in inventory.
        /// </summary>
        public int Count
        {
            get => _count;
            set => _count = value;
        }

        #endregion

        #region Constructors

        public PlayerInventoryItem() { }

        public PlayerInventoryItem(string itemUniqueId, int count)
        {
            _itemUniqueId = itemUniqueId;
            _count = count;
        }

        #endregion
    }

    /// <summary>
    /// Manages the player's collection of inventory items.
    /// Provides helpers for item lookups, modification, and clearing.
    /// </summary>
    [Serializable]
    public class PlayerInventory
    {
        #region Serialized Parameters

        [SerializeField] private List<PlayerInventoryItem> playerInventoryItems = new List<PlayerInventoryItem>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks if the inventory contains at least one of the given item.
        /// </summary>
        public bool HasItem(string itemUniqueId)
        {
            int playerInventoryCount = playerInventoryItems.Count;
            for (int i = 0; i < playerInventoryCount; i++)
            {
                if (playerInventoryItems[i].ItemUniqueId.Equals(itemUniqueId) && playerInventoryItems[i].Count > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the PlayerInventoryItem for a given unique ID, or null if not present.
        /// </summary>
        public PlayerInventoryItem GetPlayerInventoryItem(string itemUniqueId)
        {
            int playerInventoryCount = playerInventoryItems.Count;
            for (int i = 0; i < playerInventoryCount; i++)
            {
                if (playerInventoryItems[i].ItemUniqueId.Equals(itemUniqueId))
                    return playerInventoryItems[i];
            }
            return null;
        }

        /// <summary>
        /// Returns a list of all items in the inventory.
        /// </summary>
        public List<PlayerInventoryItem> GetInventoryItems()
        {
            return playerInventoryItems;
        }

        /// <summary>
        /// Increases the counts for items using another inventory as the source.
        /// </summary>
        public void IncreaseCountByInventory(PlayerInventory playerInventory)
        {
            List<PlayerInventoryItem> inventoryItems = playerInventory.playerInventoryItems;
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                IncreaseCount(inventoryItems[i].ItemUniqueId, inventoryItems[i].Count);
            }
        }

        /// <summary>
        /// Decreases the counts for items using another inventory as the source.
        /// </summary>
        public void DecreaseCountByInventory(PlayerInventory playerInventory)
        {
            List<PlayerInventoryItem> inventoryItems = playerInventory.playerInventoryItems;
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                DecreaseCount(inventoryItems[i].ItemUniqueId, inventoryItems[i].Count);
            }
        }

        /// <summary>
        /// Increases the count of a specific item by amount. Adds new entry if missing.
        /// </summary>
        public void IncreaseCount(string itemUniqueId, int amount)
        {
            PlayerInventoryItem playerInventoryItem = GetPlayerInventoryItem(itemUniqueId);
            if (playerInventoryItem == null)
            {
                playerInventoryItem = new PlayerInventoryItem(itemUniqueId, 0);
                playerInventoryItems.Add(playerInventoryItem);
            }
            playerInventoryItem.Count += amount;
        }

        /// <summary>
        /// Decreases the count of a specific item by amount. Removes entry if count reaches zero.
        /// Returns false if not enough items to decrease.
        /// </summary>
        public bool DecreaseCount(string itemUniqueId, int amount)
        {
            PlayerInventoryItem playerInventoryItem = GetPlayerInventoryItem(itemUniqueId);
            if (playerInventoryItem == null || playerInventoryItem.Count < amount)
                return false;
            playerInventoryItem.Count -= amount;
            if (playerInventoryItem.Count <= 0)
                playerInventoryItems.Remove(playerInventoryItem);
            return true;
        }

        /// <summary>
        /// Removes all items from inventory.
        /// </summary>
        public void ClearInventory()
        {
            playerInventoryItems.Clear();
        }

        #endregion
    }
}
