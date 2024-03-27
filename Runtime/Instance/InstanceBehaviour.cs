using UnityEngine;

namespace FusionUnityExtras.Runtime.Instance
{
    public class InstanceBehaviour : MonoBehaviour
    {
        [SerializeField] protected InstanceIdentityObject identity;

        protected virtual void OnEnable() => identity.AddInstance(gameObject);
        protected virtual void OnDisable() => identity.RemoveInstance(gameObject);
    }
}
