using System;
using UnityEngine;

namespace unity_extras_package.Variable
{
    [CreateAssetMenu(fileName = "New Bool Variable", menuName = "Variables/Bool")]
    public class BoolVariable : DevScriptableObject
    {
        [SerializeField] private bool value;
        public bool Value
        {
            get => value;
            set
            {
                var previous = this.value;
                this.value = value;
                OnValueChange?.Invoke(previous, value);
            }
        }
        
        public event Action<bool, bool> OnValueChange;
    }
}
