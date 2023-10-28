using System;
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
        
        // History options
        private bool _showTimestamp = true;
        private string _searchString = "";

        #endregion

        #region Preferences

        private void OnEnable()
        {
            _listenersFoldout = EditorPrefs.GetBool("EditorExtensions.EventObject.ListenersFoldout", false);
            _historyFoldout = EditorPrefs.GetBool("EditorExtensions.EventObject.HistoryFoldout", false);
            _showTimestamp = EditorPrefs.GetBool("EditorExtensions.EventObject.ShowTimestamp", true);
        }

        private void OnDisable()
        {
            EditorPrefs.SetBool("EditorExtensions.EventObject.ListenersFoldout", _listenersFoldout);
            EditorPrefs.SetBool("EditorExtensions.EventObject.HistoryFoldout", _historyFoldout);
            EditorPrefs.SetBool("EditorExtensions.EventObject.ShowTimestamp", _showTimestamp);
        }

        #endregion
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUI.BeginChangeCheck();

            // Get the target
            var eventObject = (GameEventObject)target;
            if (!target) return;
            
            DisplayButtons(eventObject);
            DisplayListeners(eventObject);
            DisplayHistory(eventObject);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DisplayButtons(GameEventObject eventObject)
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Trigger Event")) eventObject.Invoke(
                    GameEventObject.InvocationSource.Editor, 
                    null, 
                    "Invoked from the scriptable object button"
                );

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
                var horizontalStyle = new GUIStyle
                {
                    alignment = TextAnchor.MiddleLeft
                };
                
                EditorGUILayout.BeginHorizontal(horizontalStyle);
                {
                    var obj = listener.Object();
                    
                    GUILayout.Label(obj.name, EditorStyles.boldLabel);
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
            
            DrawHistoryToolbar(eventObject);

            for (int i = eventObject.InvocationHistory.Count-1; i >= 0; i--)
            {
                var invocation = eventObject.InvocationHistory[i];
                DrawHistoryField(invocation);
            }
        }

        private void DrawHistoryToolbar(GameEventObject eventObject)
        {
            EditorGUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));
            {
                if (GUILayout.Button("Clear", EditorStyles.toolbarButton)) eventObject.InvocationHistory.Clear();
                _showTimestamp = GUILayout.Toggle(_showTimestamp, "Show Timestamp", EditorStyles.toolbarButton);

                var pauseOnEventProp = serializedObject.FindProperty("pauseOnEvent");
                pauseOnEventProp.boolValue = GUILayout.Toggle(pauseOnEventProp.boolValue, "Pause On Event", EditorStyles.toolbarButton);
                
                _searchString = SearchField(_searchString);
            }
            EditorGUILayout.EndHorizontal();
        }

        private string SearchField(string content)
        {
            GUILayout.FlexibleSpace();
                
            var searchStyle = GUI.skin.FindStyle("ToolbarSeachTextField");
                
            content = GUILayout.TextField(content, searchStyle, GUILayout.MinWidth(100));

            var cancelStyle = GUI.skin.FindStyle("ToolbarSeachCancelButton");

            if (!GUILayout.Button("", cancelStyle)) return content;
                
            content = "";
            GUI.FocusControl(null);

            return content;
        }

        private void DrawHistoryField(GameEventObject.Invocation invocation)
        {
            // Perform search check
            if (!IsInSearch(invocation)) return;
            
            // Find icons
            GUIContent icon = EditorGUIUtility.IconContent("console.infoicon");
            GUIContent editorIcon = EditorGUIUtility.IconContent("d__Popup@2x");

            var horizontalStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleLeft
            };

            EditorGUILayout.BeginHorizontal(horizontalStyle);
            {
                // Draw icon
                GUILayout.Box(invocation.source == GameEventObject.InvocationSource.Editor ? editorIcon : icon);
                
                // Draw content
                EditorGUILayout.BeginVertical();
                {
                    string top = "";
                    string bottom = "";

                    // Construct top string
                    if (_showTimestamp) top += $"[{invocation.timeStamp:HH:mm:ss}] ";
                    top += $"{invocation.source}:{invocation.title}";
                    
                    // Construct bottom string
                    bottom += invocation.description;
                    
                    // Display fields
                    GUILayout.Label(top);
                    GUILayout.Label(bottom);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
            
            GUILine();
        }

        private bool IsInSearch(GameEventObject.Invocation invocation)
        {
            string compare = _searchString.ToLower();
            
            if (compare == "") return true;
            if (invocation.source.ToString().ToLower().Contains(compare)) return true;
            if (invocation.title.ToLower().Contains(compare)) return true;
            if (invocation.description.ToLower().Contains(compare)) return true;
            if (invocation.timeStamp.ToString("HH:mm:ss").ToLower().Contains(compare)) return true;
            
            return false;
        }

        private void GUILine(int lineHeight = 1)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, lineHeight);
            rect.height = lineHeight;
            EditorGUI.DrawRect(rect, new Color(.33f, .33f, .33f, 1));
        }
    }
}
