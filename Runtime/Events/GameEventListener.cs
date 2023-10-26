using UnityEngine;
using UnityEngine.Events;

namespace unity_extras_package.Events
{
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        [SerializeField] private GameEventObject eventObject;
        [SerializeField] private UnityEvent onInvoke;

        private void OnEnable() => eventObject.Subscribe(this);
        private void OnDisable() => eventObject.Unsubscribe(this);
        public void OnInvoke() => onInvoke.Invoke();
        
        public GameObject Object() => gameObject;
    }
}
