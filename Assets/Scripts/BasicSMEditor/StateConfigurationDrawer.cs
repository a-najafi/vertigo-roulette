using BasicSM;

namespace BasicSMEditor
{
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StateConfigurationBase))]
public class StateConfigurationDrawer : PropertyDrawer
{
    private static List<Type> _transitionTypes;
    private static string[] _typeNames;

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

        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty gameObjectProp = property.FindPropertyRelative("_stateGameObject");
        SerializedProperty transitionsProp = property.FindPropertyRelative("_serializedTransitions");

        float y = position.y;

        EditorGUI.PropertyField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), gameObjectProp);
        y += EditorGUIUtility.singleLineHeight + 4f;

        EditorGUI.LabelField(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), "Transitions", EditorStyles.boldLabel);
        y += EditorGUIUtility.singleLineHeight + 4f;

        for (int i = 0; i < transitionsProp.arraySize; i++)
        {
            SerializedProperty elementProp = transitionsProp.GetArrayElementAtIndex(i);
            Rect boxRect = new Rect(position.x, y, position.width, EditorGUI.GetPropertyHeight(elementProp, true) + 10);

            GUI.Box(boxRect, GUIContent.none);
            y += 5;
            EditorGUI.PropertyField(new Rect(position.x + 5, y, position.width - 10, EditorGUI.GetPropertyHeight(elementProp, true)), elementProp, GUIContent.none, true);
            y += EditorGUI.GetPropertyHeight(elementProp, true) + 5;
        }

        if (GUI.Button(new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight), "+ Add Transition"))
        {
            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < _transitionTypes.Count; i++)
            {
                int index = i;
                menu.AddItem(new GUIContent(_typeNames[i]), false, () =>
                {
                    property.serializedObject.Update();
                    transitionsProp.arraySize++;
                    var newElement = transitionsProp.GetArrayElementAtIndex(transitionsProp.arraySize - 1);
                    newElement.managedReferenceValue = Activator.CreateInstance(_transitionTypes[index]);
                    property.serializedObject.ApplyModifiedProperties();
                });
            }
            menu.ShowAsContext();
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight * 2 + 8f; // gameObject + label

        SerializedProperty transitionsProp = property.FindPropertyRelative("_serializedTransitions");
        for (int i = 0; i < transitionsProp.arraySize; i++)
        {
            SerializedProperty elementProp = transitionsProp.GetArrayElementAtIndex(i);
            height += EditorGUI.GetPropertyHeight(elementProp, true) + 10f;
        }

        height += EditorGUIUtility.singleLineHeight + 4f; // Add button
        return height;
    }
}
#endif

}