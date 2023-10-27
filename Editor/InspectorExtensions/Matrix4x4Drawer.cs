using UnityEditor;
using UnityEngine;

namespace unity_extras_package.InspectorExtensions
{
    [CustomPropertyDrawer(typeof(Matrix4x4))]
    public class Matrix4X4Drawer : PropertyDrawer
    {
        #region Static Fields

        private const float FieldWidth = 40;
        private const int FieldHeight = 16;
        private const int FieldHorizontalGap = 5;
        private const int FieldVerticalGap = 5;

        #endregion

        private float _quarterWidth;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, FieldWidth, FieldHeight), property.isExpanded, label);
            
            EditorGUI.BeginProperty(position, label, property);

            if (property.isExpanded)
            {
                // - GUI Alignment Variables
                position.y += FieldHeight + FieldVerticalGap;
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                _quarterWidth = (position.width / 4) - 5;
            
                // Get matrix object
                var matrixObj = fieldInfo.GetValue(property.serializedObject.targetObject);
                var matrix = matrixObj is Matrix4x4 x4 ? x4 : default;
                
                // Draw Matrix
                position = DrawMatrix(position, property);
                

                // Draw extra matrix info
                GUI.enabled = false;
                EditorGUI.FloatField(new Rect(position.x, position.y, position.width, FieldHeight), new GUIContent("Determinant"), matrix.determinant);
                GUI.enabled = true;

                EditorGUI.indentLevel = indent;   
            }

            EditorGUI.EndProperty();
        }
        
        private Rect DrawMatrix(Rect position, SerializedProperty property)
        {
            float labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 30;
            
            DrawMatrixRow(
                true,
                position, 
                property.FindPropertyRelative("e00"), property.FindPropertyRelative("e01"), property.FindPropertyRelative("e02"), property.FindPropertyRelative("e03")
            );
            position.y += FieldVerticalGap + FieldHeight;
            DrawMatrixRow(
                true,
                position,
                property.FindPropertyRelative("e10"), property.FindPropertyRelative("e11"), property.FindPropertyRelative("e12"), property.FindPropertyRelative("e13")
            );
            position.y += FieldVerticalGap + FieldHeight;
            DrawMatrixRow(
                true,
                position, 
                property.FindPropertyRelative("e20"), property.FindPropertyRelative("e21"), property.FindPropertyRelative("e22"), property.FindPropertyRelative("e23")
            );   
            position.y += FieldVerticalGap + FieldHeight;
            DrawMatrixRow(
                true,
                position, 
                property.FindPropertyRelative("e30"), property.FindPropertyRelative("e31"), property.FindPropertyRelative("e32"), property.FindPropertyRelative("e33")
            );   
            position.y += FieldVerticalGap + FieldHeight;

            EditorGUIUtility.labelWidth = labelWidth;

            return position;
        }

        private void DrawMatrixRow(bool enabled, Rect position, SerializedProperty var1, SerializedProperty var2, SerializedProperty var3, SerializedProperty var4)
        {
            if (!enabled) GUI.enabled = false;
            
            var m0Rect = new Rect(position.x + (_quarterWidth + FieldHorizontalGap) * 0, position.y, _quarterWidth, FieldHeight);
            var m1Rect = new Rect(position.x + (_quarterWidth + FieldHorizontalGap) * 1, position.y, _quarterWidth, FieldHeight);
            var m2Rect = new Rect(position.x + (_quarterWidth + FieldHorizontalGap) * 2, position.y, _quarterWidth, FieldHeight);
            var m3Rect = new Rect(position.x + (_quarterWidth + FieldHorizontalGap) * 3, position.y, _quarterWidth, FieldHeight);
            
            EditorGUI.PropertyField(m0Rect, var1);
            EditorGUI.PropertyField(m1Rect, var2);
            EditorGUI.PropertyField(m2Rect, var3);
            EditorGUI.PropertyField(m3Rect, var4);

            GUI.enabled = true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return
                property.isExpanded
                    ? (FieldVerticalGap + FieldHeight) * 5 +    // Matrix Display Portion
                      (FieldVerticalGap + FieldHeight):         // Determinant Display Portion
                      FieldHeight;                              // Height if Closed
        }
    }
}
