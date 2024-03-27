using UnityEngine;

namespace FusionUnityExtras.Runtime.Events
{
    public interface IGameEventListener
    {
        void OnInvoke();
        GameObject Object()
        {
            return null;
        }
    }
}
