using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor.Drawers
{
    // [CustomPropertyDrawer(typeof(ImpactData))]
    [CustomPropertyDrawer(typeof(ExpandableScriptableObject), true)]
    public class ExpandableScriptableObjectDrawer : PropertyDrawer
    {
        // Cached scriptable object editor
        private UnityEditor.Editor _editor;

        public override void OnGUI(Rect totalRect, SerializedProperty property, GUIContent label)
        {
            Rect contentRect = totalRect, foldoutRect = totalRect;

            foldoutRect.width = EditorGUIUtility.labelWidth;
            contentRect.width -= foldoutRect.width + EditorGUIUtility.standardVerticalSpacing;
            contentRect.x += foldoutRect.width + EditorGUIUtility.standardVerticalSpacing;

            // Draw foldout header
            GUIContent content = new GUIContent(label);
            bool noObject = property.objectReferenceValue == null;
            if (noObject)
            {
                if (label.text.IsNullOrWhitespace()) content.text = $"Unassigned {property.GetType().Name}";
                EditorGUI.LabelField(foldoutRect, content);
            }
            else
            {
                if (content.text.IsNullOrWhitespace()) content.text = property.objectReferenceValue.name;
                property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, property.isExpanded, content);
                EditorGUI.EndFoldoutHeaderGroup();
            }

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            // Draw property (no label)
            EditorGUI.PropertyField(contentRect, property, GUIContent.none, true);
            EditorGUI.indentLevel = indent;

            // Draw foldout properties
            if (!noObject && property.isExpanded)
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
