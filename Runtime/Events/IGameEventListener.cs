using UnityEngine;

namespace FusionUnityExtras.Events
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
