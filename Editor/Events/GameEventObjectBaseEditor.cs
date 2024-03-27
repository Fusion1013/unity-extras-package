using System;
using System.Collections.Generic;
using FusionUnityExtras.Runtime.Events;
using UnityEditor;
using UnityEngine;

namespace FusionUnityExtras.Editor.Events
{
    public abstract class GameEventObjectBaseEditor : UnityEditor.Editor
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
            var eventObject = (GameEventObjectBase)target;
            
            DisplayButtons();
            DisplayListeners();
            DisplayHistory(eventObject);

            if (EditorGUI.EndChangeCheck()) serializedObject.ApplyModifiedProperties();
        }

        private void DisplayButtons()
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Trigger Event")) InvokeEvent();
                DrawOptionalParameterField();

                if (GUILayout.Button("Print Listeners"))
                {
                    foreach (var listener in GetDelegates()) Debug.Log(listener);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        protected abstract void DrawOptionalParameterField();
        protected abstract void InvokeEvent();

        private void DisplayListeners()
        {
            var delegates = GetDelegates();
            
            _listenersFoldout = EditorGUILayout.Foldout(_listenersFoldout, $"Registered Listeners ({delegates.Count})");
            if (!_listenersFoldout) return;
            
            foreach (var listener in delegates) DrawListenerItem(listener);
        }

        protected abstract List<Delegate> GetDelegates();

        private void DrawListenerItem(Delegate del)
        {
            var horizontalStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleLeft
            };
                
            EditorGUILayout.BeginHorizontal(horizontalStyle);
            {
                var listenerTarget = del.Target;
                var methodInfo = del.Method;

                GUILayout.Label(listenerTarget + ":" + methodInfo.Name);
                if (listenerTarget is MonoBehaviour mono)
                {
                    if (GUILayout.Button("Highlight")) EditorGUIUtility.PingObject(mono);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DisplayHistory(GameEventObjectBase eventObject)
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
        
        private void DrawHistoryToolbar(GameEventObjectBase eventObject)
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

        private void DrawHistoryField(GameEventObjectBase.Invocation invocation)
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
                GUILayout.Box(invocation.source == GameEventObjectBase.InvocationSource.Editor ? editorIcon : icon);
                
                // Draw content
                EditorGUILayout.BeginVertical();
                {
                    string top = "";
                    string bottom = "";

                    // Construct top string
                    if (_showTimestamp) top += $"[{invocation.timeStamp:HH:mm:ss}] ";
                    top += $"{invocation.source}:{invocation.title}";
                    
                    // Construct bottom string
                    bottom += invocation.description + ". ";
                    if (invocation.extra != null)
                    {
                        for (int i = 0; i < invocation.extra.Length; i++)
                        {
                            bottom += $"P{i}: ";
                            bottom += invocation.extra[i];
                            if (i + 1 < invocation.extra.Length) bottom += ", ";
                        }
                    }

                    // Display fields
                    GUILayout.Label(top);
                    GUILayout.Label(bottom);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
            
            GUILine();
        }
        
        private bool IsInSearch(GameEventObjectBase.Invocation invocation)
        {
            string compare = _searchString.ToLower();
            
            if (compare == "") return true;
            if (invocation.source.ToString().ToLower().Contains(compare)) return true;
            if (invocation.title.ToLower().Contains(compare)) return true;
            if (invocation.description.ToLower().Contains(compare)) return true;
            if (invocation.timeStamp.ToString("HH:mm:ss").ToLower().Contains(compare)) return true;
            foreach (var e in invocation.extra) if (e.ToString().ToLower().Contains(compare)) return true;

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
