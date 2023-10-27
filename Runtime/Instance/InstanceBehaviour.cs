using System;
using UnityEngine;

namespace unity_extras_package.Instance
{
    public class InstanceBehaviour : MonoBehaviour
    {
        [SerializeField] protected InstanceIdentityObject identity;

        protected virtual void OnEnable() => identity.AddInstance(gameObject);
        protected virtual void OnDisable() => identity.RemoveInstance(gameObject);
    }
}
