using System.Collections.Generic;
using UnityEngine;

namespace unity_extras_package.Events
{
    [CreateAssetMenu(fileName = "New Game Event Object", menuName = "Events/Game Event Object")]
    public class GameEventObject : ScriptableObject
    {
        public List<IGameEventListener> Listeners { get; } = new();

        public void Invoke()
        {
            for (int i = Listeners.Count-1; i >= 0; i--) Listeners[i].OnInvoke();
        }

        public void Subscribe(IGameEventListener listener) => Listeners.Add(listener);
        public void Unsubscribe(IGameEventListener listener) => Listeners.Remove(listener);
    }
}
