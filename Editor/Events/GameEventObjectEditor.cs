using UnityEditor;
using UnityEngine;

namespace unity_extras_package.Events
{
    [CustomEditor(typeof(GameEventObject))]
    public class GameEventObjectEditor : Editor
    {
        #region Fields

        private bool _listenersFoldout;
        private bool _historyFoldout;

        #endregion
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Get the target
            var eventObject = (GameEventObject)target;
            if (!target) return;
            
            DisplayButtons(eventObject);
            DisplayListeners(eventObject);
            DisplayHistory(eventObject);
        }

        private void DisplayButtons(GameEventObject eventObject)
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Trigger Event")) eventObject.Invoke("Editor", "Invoked from the scriptable object button");

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
            _listenersFoldout = EditorGUILayout.Foldout(_listenersFoldout, $"Registered Listeners ({eventObject.Listeners.Count})");
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

        private void DisplayHistory(GameEventObject eventObject)
        {
            _historyFoldout =
                EditorGUILayout.Foldout(_historyFoldout, $"History ({eventObject.InvocationHistory.Count})");
            if (!_historyFoldout) return;

            for (int i = eventObject.InvocationHistory.Count-1; i >= 0; i--)
            {
                var invocation = eventObject.InvocationHistory[i];

                var horizontalStyle = new GUIStyle
                {
                    alignment = TextAnchor.MiddleLeft
                };

                EditorGUILayout.BeginHorizontal(horizontalStyle);
                {
                    GUILayout.Label($"[{invocation.timeStamp:HH:mm:ss:fff}]: ");
                    GUILayout.Label(new GUIContent(invocation.source), EditorStyles.boldLabel);
                    GUILayout.Label(new GUIContent(invocation.description));
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
