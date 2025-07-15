using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility.PropertyAttributes;

namespace VertigoRouletteMiniGame.ApplicationFlow.Inventory
{
    /// <summary>
    /// All possible item categories/types in the inventory.
    /// </summary>
    public enum ItemType
    {
        None,
        Currency,
        Chest,
        Skin,
        Upgrade,
        Consumable,
    }

    /// <summary>
    /// ScriptableObject definition for an inventory item, including addressable asset info and metadata.
    /// </summary>
    [CreateAssetMenu(menuName = "Inventory/ItemDefinition")]
    public class ItemDefinition : ScriptableObject
    {
        #region Serialized Parameters

        /// <summary>
        /// Unique ID for this item; should match the addressable key.
        /// </summary>
        [SerializeField, ReadOnly]
        private string _uniqueId = "ItemDefinition must be set to addressable";

        /// <summary>
        /// Display name of the item.
        /// </summary>
        [SerializeField]
        private string _name;

        /// <summary>
        /// Description text for the item.
        /// </summary>
        [SerializeField]
        private string _description;

        /// <summary>
        /// Sprite icon asset reference (should be in a sprite atlas).
        /// </summary>
        [SerializeField]
        private AssetReferenceAtlasedSprite _icon;

        /// <summary>
        /// Type/category of the item.
        /// </summary>
        [SerializeField]
        private ItemType _type;

        #endregion

        #region Properties

        /// <summary>
        /// Addressable sprite icon for this item.
        /// </summary>
        public AssetReferenceAtlasedSprite Icon => _icon;

        /// <summary>
        /// Item's description for UI or tooltips.
        /// </summary>
        public string Description => _description;

        /// <summary>
        /// Display name for UI.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Item type/category.
        /// </summary>
        public ItemType Type => _type;

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the internal unique ID, usually set by an addressable import/ID assignment tool.
        /// </summary>
        /// <param name="uniqueId">The new unique ID string.</param>
        public void UpdateUniqueId(string uniqueId)
        {
            _uniqueId = uniqueId;
        }

        #endregion
    }
}
