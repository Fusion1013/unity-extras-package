using System;
using Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor.Attributes
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class AlwaysSelect : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var c = serializedObject.targetObject as Component;
            if (c == null) return;
            
            SceneVisibilityManager.instance.DisablePicking(c.gameObject, true);
            SceneVisibilityManager.instance.EnablePicking(c.gameObject, false);
        }
    }
}
