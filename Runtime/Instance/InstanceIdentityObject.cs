using System;
using System.Collections.Generic;
using FusionUnityExtras.Attributes;
using UnityEngine;

namespace FusionUnityExtras.Instance
{
    [CreateAssetMenu(fileName = "New Instance Identity", menuName = "Fusion/Instance/Identity")]
    public class InstanceIdentityObject : ScriptableObject
    {
        public string identityName;
        
        [SerializeField] [ReadOnly] private string id = "";
        public string Id => id;

        public List<GameObject> Instances { get; } = new();

        private void Awake()
        {
            if (id != "") return;
            id = Guid.NewGuid().ToString();
        }

        public void AddInstance(GameObject gameObject) => Instances.Add(gameObject);
        public void RemoveInstance(GameObject gameObject) => Instances.Remove(gameObject);
    }
}
