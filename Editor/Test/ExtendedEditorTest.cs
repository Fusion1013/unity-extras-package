using unity_extras_package.InspectorExtensions;
using UnityEditor;

namespace unity_extras_package.Test
{
    [CustomEditor(typeof(TestComponent))]
    internal class ExtendedEditorTest : ExtendedEditor
    {
        protected override InspectorComponent[] RegisterComponents()
        {
            return new []
            {
                new InspectorComponent(() =>
                {
                    EditorGUILayout.Slider("Test Slider", .5f, 0, 1);
                }).Foldout("Slider Foldout"),
                new InspectorComponent(() =>
                {
                    EditorGUILayout.LabelField("Hello World!");
                }).Foldout("Hello World Label!")
            };
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawInspectorComponents();
        }
    }
}
