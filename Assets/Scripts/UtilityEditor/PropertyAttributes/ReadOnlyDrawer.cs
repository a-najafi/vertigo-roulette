using Utility.PropertyAttributes;

namespace UtilityEditor.PropertyAttributes
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Custom property drawer for <see cref="ReadOnlyAttribute"/>.
    /// Disables editing of the property in the inspector while still displaying its value.
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        #region Public Methods

        /// <summary>
        /// Renders the property field in a disabled (read-only) state.
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The SerializedProperty to make read-only.</param>
        /// <param name="label">The label of this property.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }

        #endregion
    }
}