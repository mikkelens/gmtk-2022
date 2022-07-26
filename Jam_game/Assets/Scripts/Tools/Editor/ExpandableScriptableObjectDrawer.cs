using UnityEditor;
using UnityEngine;

namespace Tools.Editor
{
    [CustomPropertyDrawer(typeof(ExpandableScriptableObject), true)]
    public class ExpandableScriptableObjectDrawer : PropertyDrawer
    {
        // Cached scriptable object editor
        private UnityEditor.Editor _editor;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect contentRect = position, foldoutRect = position;

            foldoutRect.width = EditorGUIUtility.labelWidth;
            contentRect.width -= foldoutRect.width + EditorGUIUtility.standardVerticalSpacing;
            contentRect.x += foldoutRect.width + EditorGUIUtility.standardVerticalSpacing;
            
            // Draw foldout header
            if (property.objectReferenceValue != null)
            {
                property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, property.isExpanded, label);
                EditorGUI.EndFoldoutHeaderGroup();
            }
            
            // Draw label
            EditorGUI.PropertyField(contentRect, property, GUIContent.none, true);

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