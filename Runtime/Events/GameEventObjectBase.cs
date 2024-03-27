using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace unity_extras_package.Events
{
    public abstract class GameEventObjectBase : ScriptableObject
    {

#if UNITY_EDITOR

        protected static string GetCaller(int level = 2)
        {
            var m = new StackTrace().GetFrame(level).GetMethod();
            var className = m.DeclaringType.FullName;
            var methodName = m.Name;
            return className + ":" + methodName;
        }
        
        protected void AddHistory(InvocationSource source, string title, object[] extra, string description = "")
        {
            invocationHistory.Add(new Invocation
            {
                source = source,
                title = title,
                description = description,
                timeStamp = DateTime.Now,
                extra = extra
            });
            if (pauseOnEvent && EditorApplication.isPlaying) EditorApplication.isPaused = true;
        }

        [SerializeField] [HideInInspector] private List<Invocation> invocationHistory = new();
        public List<Invocation> InvocationHistory => invocationHistory;
        [SerializeField] [HideInInspector] private bool pauseOnEvent;

        [Serializable]
        public struct Invocation
        {
            public InvocationSource source;
            public string title;
            public string description;
            public DateTime timeStamp;
            public object[] extra;
        }
#endif
        public enum InvocationSource
        {
            Runtime, Editor
        }
    }
}
