using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace VertigoRouletteMiniGame.ApplicationFlow.Inventory
{
    
    public enum ItemType
    {
        None,
        Currency,
        Chest,
        Skin,
        Upgrade,
        Consumable,
    }
    
    
    [CreateAssetMenu(menuName = "Inventory/ItemDefinition")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField,ReadOnly]private string _uniqueId = "ItemDefinition must be set to addressable";
        [SerializeField]private string _name;
        [SerializeField]private string _description;
        [SerializeField]AssetReferenceAtlasedSprite _icon;
        [SerializeField] private ItemType _type;


        public AssetReferenceAtlasedSprite Icon => _icon;
        public string Description => _description;
        public string Name => _name;
        public ItemType Type => _type;

        public void UpdateUniqueId(string uniqueId)
        {
            _uniqueId = uniqueId;
        }
        
    }
}