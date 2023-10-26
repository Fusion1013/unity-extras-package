using UnityEditor;
using UnityEngine;

namespace unity_extras_package.Events
{
    [CustomEditor(typeof(GameEventObject))]
    public class GameEventObjectEditor : UnityEditor.Editor
    {
        #region Fields

        private bool _listenersFoldout;

        #endregion
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Get the target
            var eventObject = (GameEventObject)target;
            if (!target) return;
            
            DisplayButtons(eventObject);
            DisplayListeners(eventObject);
        }

        private void DisplayButtons(GameEventObject eventObject)
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Trigger Event")) eventObject.Invoke();

                if (GUILayout.Button("Print Listeners"))
                {
                    foreach (var listener in eventObject.Listeners)
                    {
                        Debug.Log(listener);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DisplayListeners(GameEventObject eventObject)
        {
            _listenersFoldout = EditorGUILayout.Foldout(_listenersFoldout, $"Registered Listeners: {eventObject.Listeners.Count}");

            if (!_listenersFoldout) return;
            foreach (var listener in eventObject.Listeners)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    var obj = listener.Object();
                    
                    EditorGUILayout.LabelField(obj.name, EditorStyles.boldLabel);
                    if (GUILayout.Button("Highlight")) EditorGUIUtility.PingObject(obj);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
