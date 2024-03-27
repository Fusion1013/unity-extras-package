using UnityEngine;
using UnityEngine.Events;

namespace FusionUnityExtras.Runtime.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEventObject eventObject;
        [SerializeField] private UnityEvent onInvoke;

        private void OnEnable() => eventObject.Subscribe(OnInvoke);
        private void OnDisable() => eventObject.Unsubscribe(OnInvoke);
        public void OnInvoke() => onInvoke.Invoke();
    }
}
