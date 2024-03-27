using FusionUnityExtras.Runtime.Events;
using UnityEditor;
using UnityEngine;

namespace FusionUnityExtras.Editor.Events
{
    public class EventObjectEditorWindow : EditorWindow
    {
        // History options
        private bool _showTimestamp = true;
        private string _searchString = "";
        private bool _pauseOnEvent = false;
        private Vector2 _scrollPos;

        private GameEventObject _selectedObject;

        [MenuItem("Tools/Event Debugger")]
        public static void ShowWindow()
        {
            var window = GetWindow<EventObjectEditorWindow>();
            window.titleContent = new GUIContent("Event Debugger");
            window.titleContent.image = EditorGUIUtility.IconContent("d_UnityEditor.ConsoleWindow").image;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += SwitchSelection;
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= SwitchSelection;
            if (_selectedObject) _selectedObject -= OnInvoke;
        }

        private void SwitchSelection()
        {
            var selected = Selection.activeObject;

            if (_selectedObject) _selectedObject -= OnInvoke;
            
            if (selected is GameEventObject eventObject) _selectedObject = eventObject;
            else _selectedObject = null;

            if (_selectedObject) _selectedObject += OnInvoke;
            
            Repaint();
        }
        
        private void OnGUI()
        {
            if (!_selectedObject) return;
            
            DisplayHistory(_selectedObject);
        }
        
        private void DisplayHistory(GameEventObject eventObject)
        {
            DrawHistoryToolbar(eventObject);

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            {
                for (int i = eventObject.InvocationHistory.Count - 1; i >= 0; i--)
                {
                    var invocation = eventObject.InvocationHistory[i];
                    DrawHistoryField(invocation);
                }
            }
            EditorGUILayout.EndScrollView();
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

        private void DrawHistoryToolbar(GameEventObject eventObject)
        {
            EditorGUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));
            {
                if (GUILayout.Button("Clear", EditorStyles.toolbarButton)) eventObject.InvocationHistory.Clear();
                _showTimestamp = GUILayout.Toggle(_showTimestamp, "Show Timestamp", EditorStyles.toolbarButton);

                _pauseOnEvent = GUILayout.Toggle(_pauseOnEvent, "Pause On Event", EditorStyles.toolbarButton);
                
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

        public void OnInvoke()
        {
            Repaint();
        }
    }
}
