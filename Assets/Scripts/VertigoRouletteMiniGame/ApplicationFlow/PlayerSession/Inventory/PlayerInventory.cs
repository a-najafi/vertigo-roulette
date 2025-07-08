using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.Inventory
{
    [Serializable]
    public class PlayerInventoryItem
    {
        [SerializeField]private string _itemUniqueId = "Invalid";
        [SerializeField]private int _count = 0;
        public string ItemUniqueId => _itemUniqueId;
        public int Count
        {
            get => _count;
            set => _count = value;
        }

        public PlayerInventoryItem()
        {
            
        }

        public PlayerInventoryItem(string itemUniqueId, int count)
        {
            _itemUniqueId = itemUniqueId;
            _count = count;
        }
        
        
    }
    
    [Serializable]
    public class PlayerInventory
    {
        
        [SerializeField]private List<PlayerInventoryItem> playerInventoryItems = new List<PlayerInventoryItem>();
        
        
        

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
        
        public bool DecreaseCount(string itemUniqueId, int amount)
        {
            PlayerInventoryItem playerInventoryItem = GetPlayerInventoryItem(itemUniqueId);
            if (playerInventoryItem == null || playerInventoryItem.Count < amount)
                return false;
            playerInventoryItem.Count -= amount;
            return true;
        }
        
        
    }
}