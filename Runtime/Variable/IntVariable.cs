using System;
using UnityEngine;

namespace unity_extras_package.Variable
{
    [CreateAssetMenu(fileName = "New Int Variable", menuName = "Variables/Int")]
    public class IntVariable : DevScriptableObject
    {
        [SerializeField] private int value;
        public int Value
        {
            get => value;
            set
            {
                var previous = this.value;
                this.value = value;
                OnValueChange?.Invoke(previous, value);
            }
        }
        
        public event Action<int, int> OnValueChange;
    }
}
