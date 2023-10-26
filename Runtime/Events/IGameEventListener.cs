using UnityEngine;

namespace Events
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
