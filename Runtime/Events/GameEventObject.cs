using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace unity_extras_package.Events
{
    [CreateAssetMenu(fileName = "New Game Event Object", menuName = "Events/Game Event Object")]
    public class GameEventObject : ScriptableObject
    {
        public List<IGameEventListener> Listeners { get; } = new();

#if UNITY_EDITOR
        [SerializeField] [HideInInspector] private List<Invocation> invocationHistory = new();
        public List<Invocation> InvocationHistory => invocationHistory; 
        [SerializeField] [HideInInspector] private bool pauseOnEvent;
#endif

        public void Invoke(GameObject sourceObject, string description = "") => Invoke(InvocationSource.Runtime, sourceObject, description);
        public void Invoke(InvocationSource source, GameObject sourceObject, string description = "")
        {
            for (int i = Listeners.Count-1; i >= 0; i--) Listeners[i].OnInvoke();

#if UNITY_EDITOR
            invocationHistory.Add(new Invocation()
            {
                source = source,
                title = source == InvocationSource.Runtime ? sourceObject.name : "Editor",
                description = description,
                timeStamp = DateTime.Now
            });
            if (pauseOnEvent && EditorApplication.isPlaying) EditorApplication.isPaused = true;
#endif
        }

        public void Subscribe(IGameEventListener listener) => Listeners.Add(listener);
        public void Unsubscribe(IGameEventListener listener) => Listeners.Remove(listener);

#if UNITY_EDITOR
        [Serializable]
        public struct Invocation
        {
            public InvocationSource source;
            public string title;
            public string description;
            public DateTime timeStamp;
        }
#endif
        public enum InvocationSource
        {
            Runtime, Editor
        }
    }
}
