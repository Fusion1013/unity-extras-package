using System;
using System.Collections.Generic;
using UnityEngine;

namespace unity_extras_package.Events
{
    [CreateAssetMenu(fileName = "New Game Event Object", menuName = "Events/Game Event Object")]
    public class GameEventObject : ScriptableObject
    {
        public List<IGameEventListener> Listeners { get; } = new();

#if UNITY_EDITOR
        public List<Invocation> InvocationHistory { get; } = new();
#endif

        public void Invoke(GameObject source, string description = "") => Invoke(source.name, description);
        public void Invoke(string source, string description = "")
        {
            for (int i = Listeners.Count-1; i >= 0; i--) Listeners[i].OnInvoke();

#if UNITY_EDITOR
            InvocationHistory.Add(new Invocation()
            {
                source = source,
                description = description,
                timeStamp = DateTime.Now
            });
#endif
        }

        public void Subscribe(IGameEventListener listener) => Listeners.Add(listener);
        public void Unsubscribe(IGameEventListener listener) => Listeners.Remove(listener);

#if UNITY_EDITOR
        [Serializable]
        public struct Invocation
        {
            public string source;
            public string description;
            public DateTime timeStamp;
        }
#endif
    }
}
