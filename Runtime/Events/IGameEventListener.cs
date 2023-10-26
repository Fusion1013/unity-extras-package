using UnityEngine;

namespace unity_extras_package.Events
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
