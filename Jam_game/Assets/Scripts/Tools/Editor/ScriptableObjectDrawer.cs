using Gameplay.Attacks;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor
{
    [CustomPropertyDrawer(typeof(Weapon), true)]
    public class ScriptableObjectDrawer : PropertyDrawer
    {
        // Cached scriptable object editor
        private UnityEditor.Editor _editor;

        // private static readonly Type[] BannedTypes = {
        //     typeof(StatType),
        //     typeof(StatTypeCollection),
        // };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool expandable = property.objectReferenceValue != null /*&& !BannedTypes.Contains(property.objectReferenceValue.GetType())*/;
            
            // Draw foldout arrow
            if (expandable)
            {
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
            }
            
            // Draw label
            if (expandable) EditorGUI.indentLevel++;
            EditorGUI.PropertyField(position, property, label, true);
            if (expandable) EditorGUI.indentLevel--;
            
            // Draw foldout properties
            if (property.isExpanded)
            {
                // Make child fields be indented
                EditorGUI.indentLevel++;
             
                // Draw object properties
                if (!_editor)
                    UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref _editor);
                _editor.OnInspectorGUI();
             
                // Set indent back to what it was
                EditorGUI.indentLevel--;
            }
        }
    }
}