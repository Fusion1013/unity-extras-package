using System;
using System.Linq;
using System.Reflection;
using FusionUnityExtras.QuickButtons;
using UnityEditor;
using UnityEngine;

// Initial Concept by http://www.reddit.com/user/zaikman
// Revised by http://www.reddit.com/user/quarkism

namespace FusionUnityExtras.Editor.QuickButtons
{
    [CustomEditor(typeof (MonoBehaviour), true)]
    public class EditorButton : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
 
            var mono = target as MonoBehaviour;
 
            var methods = mono.GetType()
                .GetMembers(BindingFlags.Instance | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                            BindingFlags.NonPublic)
                .Where(o => Attribute.IsDefined(o, typeof (EditorButtonAttribute)));
 
            foreach (var memberInfo in methods)
            {
                if (GUILayout.Button(memberInfo.Name))
                {
                    var method = memberInfo as MethodInfo;
                    method?.Invoke(mono, null);
                }
            }
        }
    }
}
