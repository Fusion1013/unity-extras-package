using System;
using UnityEngine;

namespace unity_extras_package.Variable
{
    [CreateAssetMenu(fileName = "New GameObject Variable", menuName = "Variables/GameObject")]
    public class GameObjectVariable : DevScriptableObject
    {
        [SerializeField] private GameObject value;
        public GameObject Value
        {
            get => value;
            set
            {
                var previous = this.value;
                this.value = value;
                OnValueChange?.Invoke(previous, value);
            }
        }
        
        public event Action<GameObject, GameObject> OnValueChange;
    }
}
