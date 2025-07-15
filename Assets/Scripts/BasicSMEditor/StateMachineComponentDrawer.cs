using BasicSM;

namespace BasicSMEditor
{
#if UNITY_EDITOR
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Custom editor for StateMachineComponentBase.
    /// Provides a rich inspector for managing state configurations and transitions.
    /// </summary>
    [CustomEditor(typeof(StateMachineComponentBase), true)]
    public class StateMachineComponentEditor : Editor
    {
        #region Non Serialized Parameters

        /// <summary>
        /// Serialized property for the list of state configurations.
        /// </summary>
        private SerializedProperty _stateConfigurationsProp;

        /// <summary>
        /// List of available transition types for new state configurations.
        /// </summary>
        private static List<Type> _transitionTypes;

        /// <summary>
        /// Display names of transition types.
        /// </summary>
        private static string[] _transitionTypeNames;

        #endregion

        #region Unity Methods

        /// <summary>
        /// Initializes cached references and type arrays on enable.
        /// </summary>
        private void OnEnable()
        {
            _stateConfigurationsProp = serializedObject.FindProperty("_serializedStateConfigurations");

            _transitionTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(StateTransitionBase).IsAssignableFrom(t) && !t.IsAbstract)
                .ToList();

            _transitionTypeNames = _transitionTypes.Select(t => t.Name).ToArray();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draws the custom inspector interface for StateMachineComponentBase.
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspectorExcluding("_serializedStateConfigurations");

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("State Configurations", EditorStyles.boldLabel);

            for (int i = 0; i < _stateConfigurationsProp.arraySize; i++)
            {
                SerializedProperty elementProp = _stateConfigurationsProp.GetArrayElementAtIndex(i);

                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.LabelField($"State Configuration Element {i}", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(elementProp, GUIContent.none, true);
                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("+ Add State Configuration"))
            {
                _stateConfigurationsProp.arraySize++;
                serializedObject.ApplyModifiedProperties();

                var boxed = new StateConfigurationBase();
                var listField = typeof(StateConfigurationBase).GetField("_serializedTransitions",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (listField != null && _transitionTypes.Count > 0)
                {
                    var defaultList = new List<StateTransitionBase>
                    {
                        (StateTransitionBase)Activator.CreateInstance(_transitionTypes[0])
                    };
                    listField.SetValue(boxed, defaultList);
                }

                var obj = target as StateMachineComponentBase;
                var configField = obj.GetType().GetField("_serializedStateConfigurations",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                var runtimeList = configField?.GetValue(obj) as List<StateConfigurationBase>;
                if (runtimeList != null && boxed is StateConfigurationBase typedConfig)
                {
                    runtimeList[_stateConfigurationsProp.arraySize - 1] = typedConfig;
                    configField.SetValue(obj, runtimeList);
                    EditorUtility.SetDirty(obj);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Draws the default inspector, excluding specified properties.
        /// </summary>
        /// <param name="excludedProps">Property names to exclude.</param>
        private void DrawDefaultInspectorExcluding(params string[] excludedProps)
        {
            SerializedProperty prop = serializedObject.GetIterator();
            bool enterChildren = true;

            while (prop.NextVisible(enterChildren))
            {
                if (Array.Exists(excludedProps, p => p == prop.name))
                    continue;

                EditorGUILayout.PropertyField(prop, true);
                enterChildren = false;
            }
        }

        #endregion
    }
#endif

}
