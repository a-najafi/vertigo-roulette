using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{
    /// <summary>
    /// Design template for a single player inventory item, used in session starting config.
    /// </summary>
    [Serializable]
    public class PlayerInventoryItemDesign
    {
        #region Serialized Parameters

        [SerializeField] private AssetReference _itemDefinitionAssetReference;
        [SerializeField] private int _count = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Number of items to start with.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Addressable reference to the ItemDefinition asset.
        /// </summary>
        public AssetReference ItemDefinitionAssetReference => _itemDefinitionAssetReference;

        #endregion
    }

    /// <summary>
    /// ScriptableObject for setting up a new player's starting inventory.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerSessionStartingConfig", menuName = "PlayerSession/PlayerSessionStartingConfig")]
    public class PlayerSessionStartingConfig : ScriptableObject
    {
        #region Serialized Parameters

        [SerializeField] private List<PlayerInventoryItemDesign> _playerInventoryItemDesigns = new List<PlayerInventoryItemDesign>();

        #endregion

        #region Properties

        /// <summary>
        /// The list of inventory items and counts to give a new player/session.
        /// </summary>
        public List<PlayerInventoryItemDesign> PlayerInventoryItemDesigns => _playerInventoryItemDesigns;

        #endregion
    }
}