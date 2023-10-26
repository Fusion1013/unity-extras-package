using UnityEditor;
using UnityEngine;

// Modified from:
// https://gist.github.com/mandarinx/ed733369fbb2eea6c7fa9e3da65a0e17

namespace unity_extras_package.InspectorExtensions
{
    [CustomEditor(typeof(MeshFilter))]
    public class NormalsVisualizer : ExtendedEditor
    {
        private const string     EditorPrefLengthKey = "_normals_length";
        private const string     EditorPrefVisualizeKey = "_visualize_normals";
        private const string     EditorPrefColorPrefixKey = "_normals_color";
        private const string     EditorPrefMaxIterationsKey = "_normals_max_draw_iterations";
        private       Mesh       _mesh;
        private       MeshFilter _mf;
        private       Vector3[]  _verts;
        private       Vector3[]  _normals;
        private       float      _normalsLength = 1f;
        private       int        _maxDrawIterations = 1000;
        private       bool       _visualizeNormals = false;
        private       Color      _normalsColor = Color.yellow;

        private bool _extended;
        private float _currentIterations;

        private void OnEnable() {
            _mf   = target as MeshFilter;
            if (_mf != null) {
                _mesh = _mf.sharedMesh;
            }

            _visualizeNormals = EditorPrefs.GetBool(EditorPrefVisualizeKey);
            _normalsLength = EditorPrefs.GetFloat(EditorPrefLengthKey);
            _maxDrawIterations = EditorPrefs.GetInt(EditorPrefMaxIterationsKey, 1000);
            
            // Color
            float r = EditorPrefs.GetFloat(EditorPrefColorPrefixKey + "_R", 1);
            float g = EditorPrefs.GetFloat(EditorPrefColorPrefixKey + "_G", 1);
            float b = EditorPrefs.GetFloat(EditorPrefColorPrefixKey + "_B", 0);
            _normalsColor = new Color(r, g, b);
        }

        private void OnSceneGUI() {
            if (_mesh == null) return;

            Handles.matrix = _mf.transform.localToWorldMatrix;
            Handles.color = _normalsColor;
            _verts = _mesh.vertices;
            _normals = _mesh.normals;
            int len = _mesh.vertexCount;
            _currentIterations = Mathf.Min(len, _maxDrawIterations);
            
            if (!_visualizeNormals) return;

            for (int i = 0; i < len && i < _maxDrawIterations; i++) {
                Handles.DrawLine(_verts[i], _verts[i] + _normals[i] * _normalsLength);
            }
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            {
                _extended = EditorGUILayout.Foldout(_extended, $"Normals ({_currentIterations})");
                
                EditorGUI.BeginChangeCheck();
                _visualizeNormals = EditorGUILayout.Toggle("Visualize Normals", _visualizeNormals);
            }
            EditorGUILayout.EndHorizontal();
            
            if (_extended)
            {
                _maxDrawIterations = EditorGUILayout.IntField("Max Iterations", _maxDrawIterations);
                _normalsLength = EditorGUILayout.FloatField("Normals length", _normalsLength);
                _normalsColor = EditorGUILayout.ColorField("Normals Color", _normalsColor);
            }
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool(EditorPrefVisualizeKey, _visualizeNormals);
                EditorPrefs.SetFloat(EditorPrefLengthKey, _normalsLength);
                EditorPrefs.SetInt(EditorPrefMaxIterationsKey, _maxDrawIterations);
                        
                // Color
                EditorPrefs.SetFloat(EditorPrefColorPrefixKey + "_R", _normalsColor.r);
                EditorPrefs.SetFloat(EditorPrefColorPrefixKey + "_G", _normalsColor.g);
                EditorPrefs.SetFloat(EditorPrefColorPrefixKey + "_B", _normalsColor.b);
                        
                SceneView.RepaintAll();
            }
        }
    }
}
