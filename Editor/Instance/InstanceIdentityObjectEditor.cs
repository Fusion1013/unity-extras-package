using FusionUnityExtras.Runtime.Instance;
using UnityEditor;
using UnityEngine;

namespace FusionUnityExtras.Editor.Instance
{
    [CustomEditor(typeof(InstanceIdentityObject))]
    public class InstanceIdentityObjectEditor : UnityEditor.Editor
    {
        private bool _instanceListFoldout;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var identityObject = (InstanceIdentityObject)target;
            DrawInstanceList(identityObject);
        }

        private void DrawInstanceList(InstanceIdentityObject identityObject)
        {
            _instanceListFoldout = EditorGUILayout.Foldout(_instanceListFoldout, $"Instances ({identityObject.Instances.Count})");
            if (!_instanceListFoldout) return;

            foreach (var instance in identityObject.Instances)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(instance.name, EditorStyles.boldLabel);
                    if (GUILayout.Button("Highlight")) EditorGUIUtility.PingObject(instance);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
