// Editor/SerializeReferenceDrawer.cs
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BasicSM;
using UnityEditor;
using UnityEngine;

namespace BasicSMEditor
{

    [CustomPropertyDrawer(typeof(StateTransitionBase), true)]
    public class StateTransitionDrawer : PropertyDrawer
    {
        private static List<Type> _derivedTypes;
        private static string[] _typeNames;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnsureTypesLoaded();

            Rect dropdownRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            Rect fieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width,
                position.height - EditorGUIUtility.singleLineHeight - 2);

            EditorGUI.BeginProperty(position, label, property);

            int currentIndex = GetTypeIndex(property.managedReferenceFullTypename);

// Draw dropdown or fallback
            int newIndex;
            if (currentIndex >= 0)
            {
                newIndex = EditorGUI.Popup(dropdownRect, "Transition Type", currentIndex, _typeNames);
            }
            else
            {
                string fallbackLabel = ExtractTypeName(property.managedReferenceFullTypename);
                EditorGUI.LabelField(dropdownRect, "Transition Type", fallbackLabel ?? "None");
                newIndex = EditorGUI.Popup(new Rect(dropdownRect.x, dropdownRect.y + EditorGUIUtility.singleLineHeight, dropdownRect.width, dropdownRect.height), -1, _typeNames);
            }


            if (newIndex != currentIndex && newIndex >= 0)
            {
                property.managedReferenceValue = Activator.CreateInstance(_derivedTypes[newIndex]);
                property.serializedObject.ApplyModifiedProperties();
            }

            // Draw the actual object fields
            if (property.managedReferenceValue != null)
            {
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(fieldRect, property, GUIContent.none, true);
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }
        private static string ExtractTypeName(string fullTypeName)
        {
            if (string.IsNullOrEmpty(fullTypeName)) return null;
            string clean = fullTypeName.Split(',')[0];
            int lastDot = clean.LastIndexOf('.');
            return lastDot >= 0 ? clean.Substring(lastDot + 1) : clean;
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.managedReferenceValue == null)
                return EditorGUIUtility.singleLineHeight + 4;

            return EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.singleLineHeight + 4;
        }

        private static void EnsureTypesLoaded()
        {
            if (_derivedTypes != null && _typeNames != null) return;

            _derivedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract && typeof(StateTransitionBase).IsAssignableFrom(t))
                .ToList();

            _typeNames = _derivedTypes.Select(t => t.Name).ToArray();
        }

        private static int GetTypeIndex(string fullTypeName)
        {
            if (string.IsNullOrEmpty(fullTypeName))
                return -1;

            string cleanName = fullTypeName.Split(',')[0]; // Strip assembly info
            return _derivedTypes.FindIndex(t => t.FullName == cleanName);
        }
    }
}
#endif
