using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility.Addressable;

namespace VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI
{
    public class PlayerInventoryDisplay : MonoBehaviour
    {
        [SerializeField]private GameObject _rewardDisplayPrefab;
        [SerializeField] private RectTransform _rewardDisplayParrent;
        [SerializeField]private GameObject _tabPrefab;
        [SerializeField]private RectTransform _tabsParent;

        private Dictionary<string, RewardDisplay> _rewardDisplayDictionary = new Dictionary<string, RewardDisplay>();
        private Dictionary<ItemType, PlayerInventoryDisplayTab> tabDisplayDictionary = new Dictionary<ItemType, PlayerInventoryDisplayTab>();

        public void FilterByTab(ItemType itemType)
        {
            foreach (var rewardDisplay in _rewardDisplayDictionary)
            {
                rewardDisplay.Value.gameObject.SetActive(itemType == ItemType.None || itemType == rewardDisplay.Value.ItemDefinition.Type);
            }

            foreach (var inventoryDisplayTab in tabDisplayDictionary)
            {
                inventoryDisplayTab.Value.SetEnabled(inventoryDisplayTab.Key == itemType);    
            }
            
        }
        

        public IEnumerator Initialize(PlayerInventory playerInventory)
        {
            List<PlayerInventoryItem> playerInventoryItems = playerInventory.GetInventoryItems();
            int playerInventoryItemsCount = playerInventoryItems.Count;

            foreach (ItemType itemType in System.Enum.GetValues(typeof(ItemType)))
            {
                if (!tabDisplayDictionary.ContainsKey(itemType))
                {
                    GameObject tab = Instantiate(_tabPrefab, _tabsParent);
                    PlayerInventoryDisplayTab tabDisplay = tab.GetComponent<PlayerInventoryDisplayTab>();
                    tabDisplay.Initialize(this, itemType);
                    tabDisplayDictionary.Add(itemType, tabDisplay);
                }
            }

            //clear out any display that will not be used
            List<string> currentDisplayKeys = _rewardDisplayDictionary.Keys.ToList();
            for (int i = 0; i < currentDisplayKeys.Count; i++)
            {
                if (!playerInventory.HasItem(currentDisplayKeys[i]))
                {
                    Destroy(_rewardDisplayDictionary[currentDisplayKeys[i]].gameObject);
                    _rewardDisplayDictionary.Remove(currentDisplayKeys[i]);
                }
            }
            
            for (int i = 0; i < playerInventoryItemsCount; i++)
            {
                var loadItemDefinitionHandle = new AssetLoadResult<ItemDefinition>();
                yield return AddressableAssetManager.LoadAsset<ItemDefinition>(
                    new AssetReference(playerInventoryItems[i].ItemUniqueId), loadItemDefinitionHandle);

                if (!_rewardDisplayDictionary.ContainsKey(playerInventoryItems[i].ItemUniqueId))
                {
                    GameObject rewardDisplayGameObject = Instantiate(_rewardDisplayPrefab, _rewardDisplayParrent);
                    RewardDisplay rewardDisplay = rewardDisplayGameObject.GetComponent<RewardDisplay>();
                    _rewardDisplayDictionary.Add(playerInventoryItems[i].ItemUniqueId, rewardDisplay);
                }
                yield return _rewardDisplayDictionary[playerInventoryItems[i].ItemUniqueId].Initialize(loadItemDefinitionHandle.Asset,playerInventoryItems[i].Count );
                _rewardDisplayDictionary[playerInventoryItems[i].ItemUniqueId].gameObject.SetActive(true);
                
            }
        }

    }
}