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
        private static List<Type> _transitionTypes;
        private static string[] _typeNames;
        private const float PADDING = 6f;
        private const float HEADER_HEIGHT = 18f;

        private void EnsureTypeCache()
        {
            if (_transitionTypes != null) return;

            _transitionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => asm.GetTypes())
                .Where(t => typeof(StateTransitionBase).IsAssignableFrom(t) && !t.IsAbstract)
                .ToList();

            _typeNames = _transitionTypes.Select(t => t.Name).ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnsureTypeCache();
            var typeRect = new Rect(position.x, position.y, position.width, HEADER_HEIGHT);
            var boxRect = new Rect(position.x, position.y, position.width, GetPropertyHeight(property, label));

            EditorGUI.DrawRect(boxRect, new Color(0.15f, 0.15f, 0.15f));
            GUI.Box(boxRect, GUIContent.none);

            EditorGUI.BeginProperty(position, label, property);

            var currentType = property.managedReferenceValue?.GetType();
            int currentIndex = Mathf.Max(0, _transitionTypes.FindIndex(t => t == currentType));
            int newIndex = EditorGUI.Popup(typeRect, "Transition Type", currentIndex, _typeNames);

            if ((property.managedReferenceValue == null || currentIndex != newIndex) && newIndex >= 0)
            {
                property.managedReferenceValue = Activator.CreateInstance(_transitionTypes[newIndex]);
                property.serializedObject.ApplyModifiedProperties();
            }

            if (property.managedReferenceValue != null)
            {
                var contentRect = new Rect(position.x + PADDING, typeRect.yMax + 2, position.width - 2 * PADDING,
                    EditorGUI.GetPropertyHeight(property, true) - HEADER_HEIGHT - PADDING);
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(contentRect, property, GUIContent.none, true);
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = HEADER_HEIGHT + PADDING;
            if (property.managedReferenceValue != null)
                height += EditorGUI.GetPropertyHeight(property, true);
            return height;
        }
    }

}
#endif
