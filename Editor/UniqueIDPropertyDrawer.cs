using System;
using UnityEditor;
using UnityEngine;

namespace FusionUnityExtras.Editor
{
    [CustomPropertyDrawer(typeof(UniqueID))]
    public class UniqueIDPropertyDrawer : PropertyDrawer
    {
        private const float ButtonWidth = 120;
        private const float Padding = 2;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            GUI.enabled = false;
            Rect valueRect = position;
            valueRect.width -= ButtonWidth + Padding;
            SerializedProperty idProperty = property.FindPropertyRelative("value");
            if (string.IsNullOrEmpty(idProperty.stringValue))
            {
                idProperty.stringValue = Guid.NewGuid().ToString();
            }
            EditorGUI.PropertyField(valueRect, idProperty, GUIContent.none);

            GUI.enabled = true;

            Rect buttonRect = position;
            buttonRect.x += position.width - ButtonWidth;
            buttonRect.width = ButtonWidth;

            if (GUI.Button(buttonRect, "Copy to Clipboard"))
            {
                EditorGUIUtility.systemCopyBuffer = idProperty.stringValue;
            }
            
            EditorGUI.EndProperty();
        }
    }
}