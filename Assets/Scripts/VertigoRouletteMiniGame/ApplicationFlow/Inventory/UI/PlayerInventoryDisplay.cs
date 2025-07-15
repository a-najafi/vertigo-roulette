using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility.Addressable;

namespace VertigoRouletteMiniGame.ApplicationFlow.Inventory.UI
{
    /// <summary>
    /// Handles display and filtering of player inventory items in the UI.
    /// Dynamically creates and manages reward displays and tab displays.
    /// </summary>
    public class PlayerInventoryDisplay : MonoBehaviour
    {
        #region Serialized Parameters

        /// <summary>
        /// Prefab for reward display entries.
        /// </summary>
        [SerializeField] private GameObject _rewardDisplayPrefab;

        /// <summary>
        /// Parent transform for instantiated reward displays.
        /// </summary>
        [SerializeField] private RectTransform _rewardDisplayParrent;

        /// <summary>
        /// Prefab for item type filter tabs.
        /// </summary>
        [SerializeField] private GameObject _tabPrefab;

        /// <summary>
        /// Parent transform for instantiated tabs.
        /// </summary>
        [SerializeField] private RectTransform _tabsParent;

        #endregion

        #region Non Serialized Parameters

        /// <summary>
        /// Maps item unique ID to their corresponding reward display components.
        /// </summary>
        private Dictionary<string, RewardDisplay> _rewardDisplayDictionary = new Dictionary<string, RewardDisplay>();

        /// <summary>
        /// Maps item type to their corresponding tab components.
        /// </summary>
        private Dictionary<ItemType, PlayerInventoryDisplayTab> tabDisplayDictionary = new Dictionary<ItemType, PlayerInventoryDisplayTab>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the reward display by item type and updates tab selection visuals.
        /// </summary>
        /// <param name="itemType">The item type to filter by.</param>
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

        /// <summary>
        /// Initializes the inventory display, creating tabs and reward displays as needed.
        /// Loads ItemDefinitions using Addressables.
        /// </summary>
        /// <param name="playerInventory">The inventory to display.</param>
        /// <returns>Coroutine enumerator.</returns>
        public IEnumerator Initialize(PlayerInventory playerInventory)
        {
            List<PlayerInventoryItem> playerInventoryItems = playerInventory.GetInventoryItems();
            int playerInventoryItemsCount = playerInventoryItems.Count;

            // Ensure all tabs are present
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

            // Remove unused reward displays
            List<string> currentDisplayKeys = _rewardDisplayDictionary.Keys.ToList();
            for (int i = 0; i < currentDisplayKeys.Count; i++)
            {
                if (!playerInventory.HasItem(currentDisplayKeys[i]))
                {
                    Destroy(_rewardDisplayDictionary[currentDisplayKeys[i]].gameObject);
                    _rewardDisplayDictionary.Remove(currentDisplayKeys[i]);
                }
            }
            
            // Add or update reward displays for each inventory item
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
                yield return _rewardDisplayDictionary[playerInventoryItems[i].ItemUniqueId]
                    .Initialize(loadItemDefinitionHandle.Asset, playerInventoryItems[i].Count);
                _rewardDisplayDictionary[playerInventoryItems[i].ItemUniqueId].gameObject.SetActive(true);
            }
        }

        #endregion
    }
}
