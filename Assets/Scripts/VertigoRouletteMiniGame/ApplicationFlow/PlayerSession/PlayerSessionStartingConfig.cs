using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace VertigoRouletteMiniGame.ApplicationFlow.PlayerSession
{

    [Serializable]
    public class PlayerInventoryItemDesign
    {
        [SerializeField]private AssetReference _itemDefinitionAssetReference;
        [SerializeField] private int _count = 0;

        public int Count => _count;

        public AssetReference ItemDefinitionAssetReference  => _itemDefinitionAssetReference;
    }
    [CreateAssetMenu(fileName = "PlayerSessionStartingConfig", menuName = "PlayerSession/PlayerSessionStartingConfig")]
    public class PlayerSessionStartingConfig : ScriptableObject
    {
        
        
        
     [SerializeField]private List<PlayerInventoryItemDesign> _playerInventoryItemDesigns = new List<PlayerInventoryItemDesign>();
     
     public List<PlayerInventoryItemDesign> PlayerInventoryItemDesigns => _playerInventoryItemDesigns;
     
    }
}