
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;
using VertigoRouletteMiniGame.ApplicationFlow.PlayerSession.Inventory;

namespace VertigoRouletteMiniGameEditor
{
    [CustomEditor(typeof(ItemDefinition))]
    public class ItemDefinitionEditor : Editor
    {
        private ItemDefinition itemDefinition;

        private void OnEnable()
        {
            itemDefinition = (ItemDefinition)target;
            ValidateAddress();
        }

        private void OnValidate()
        {
            ValidateAddress();
        }

        protected override void OnHeaderGUI()
        {
            base.OnHeaderGUI();
            ValidateAddress();
        }

        private void ValidateAddress()
        {
            if (itemDefinition != null)
            {
                var path = AssetDatabase.GetAssetPath(itemDefinition);
                var guid = AssetDatabase.AssetPathToGUID(path);
                var settings = AddressableAssetSettingsDefaultObject.Settings;
                var entry = settings.FindAssetEntry(guid);
                if(entry != null)
                    itemDefinition.UpdateUniqueId(entry.address);
                else
                {
                    itemDefinition.UpdateUniqueId("ItemDefinition must be set to addressable");
                }
            }
        }
        
    }

}