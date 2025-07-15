using UnityEditor;
using UnityEditor.AddressableAssets;
using VertigoRouletteMiniGame.ApplicationFlow.Inventory;

namespace VertigoRouletteMiniGameEditor.ApplicationFlow.PlayerSession.Inventory
{
    /// <summary>
    /// Custom inspector for <see cref="ItemDefinition"/> that automatically validates and assigns its Addressable GUID.
    /// </summary>
    [CustomEditor(typeof(ItemDefinition))]
    public class ItemDefinitionEditor : Editor
    {
        #region Non Serialized Parameters

        private ItemDefinition itemDefinition;

        #endregion

        #region Unity Events

        /// <summary>
        /// Called when the editor is enabled.
        /// Initializes the target and validates the addressable GUID.
        /// </summary>
        private void OnEnable()
        {
            itemDefinition = (ItemDefinition)target;
            ValidateAddress();
        }

        /// <summary>
        /// Called when script values are changed in the inspector.
        /// Ensures the addressable GUID is up to date.
        /// </summary>
        private void OnValidate()
        {
            ValidateAddress();
        }

        /// <summary>
        /// Draws the script header and revalidates the addressable GUID.
        /// </summary>
        protected override void OnHeaderGUI()
        {
            base.OnHeaderGUI();
            ValidateAddress();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validates the Addressable GUID and updates the ItemDefinition's unique ID accordingly.
        /// </summary>
        private void ValidateAddress()
        {
            if (itemDefinition != null)
            {
                // Get asset path and GUID for the target ItemDefinition
                var path = AssetDatabase.GetAssetPath(itemDefinition);
                var guid = AssetDatabase.AssetPathToGUID(path);

                // Retrieve Addressable Asset Settings and entry for the GUID
                var settings = AddressableAssetSettingsDefaultObject.Settings;
                var entry = settings.FindAssetEntry(guid);

                if (entry != null)
                {
                    // Assign the found GUID to the item definition
                    itemDefinition.UpdateUniqueId(entry.guid);
                }
                else
                {
                    // Not addressable; set a warning string
                    itemDefinition.UpdateUniqueId("ItemDefinition must be set to addressable");
                }
            }
        }

        #endregion
    }
}
