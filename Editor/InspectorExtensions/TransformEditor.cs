using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Mathf;

namespace FusionUnityExtras.Editor.InspectorExtensions
{
    [CustomEditor(typeof(Transform))]
    [CanEditMultipleObjects]
    public class TransformEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor _defaultEditor;
        
        private int _snapDistance;
        private bool _isExtended;

        private void OnEnable()
        {
            if (targets.Length <= 0) return;
            if (targets.Any(t => t == null)) return;
            var editorType = Type.GetType("UnityEditor.TransformInspector, UnityEditor");
            if (editorType == null) return;
            _defaultEditor = CreateEditor(targets, editorType);
            if (_defaultEditor == null) return;

            // Load variables
            _snapDistance = EditorPrefs.GetInt("transform_snap_distance", 1);
            _isExtended = EditorPrefs.GetBool("transform_foldout_extended", false);
        }

        private void OnDisable()
        {
            EditorPrefs.SetInt("transform_snap_distance", _snapDistance);
            EditorPrefs.SetBool("transform_foldout_extended", _isExtended);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUI.BeginChangeCheck();
            
            // Begin the custom top part of the inspector
            
            // Draw the default transform inspector
            _defaultEditor.OnInspectorGUI();
            
            // Begin the custom bottom part of the inspector

            EditorGUILayout.Space();

            _isExtended = EditorGUILayout.Foldout(_isExtended, "Extensions");
            if (_isExtended)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    _snapDistance = EditorGUILayout.IntField("Snap Distance", _snapDistance);
                
                    if (GUILayout.Button("Snap Cartesian"))
                    {
                        Transform tr = (Transform)target;
                
                        var pos = tr.position;
                        tr.position = new Vector3(
                            Round(pos.x / _snapDistance) * _snapDistance, 
                            Round(pos.y / _snapDistance) * _snapDistance, 
                            Round(pos.z / _snapDistance) * _snapDistance
                        );
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Update Transform Component");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
