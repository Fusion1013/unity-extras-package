using System;
using UnityEngine;

namespace unity_extras_package.Variable
{
    [CreateAssetMenu(fileName = "New Float Variable", menuName = "Variables/Float")]
    public class FloatVariable : DevScriptableObject
    {
        [SerializeField] private float value;
        public float Value
        {
            get => value;
            set
            {
                var previous = this.value;
                this.value = value;
                OnValueChange?.Invoke(previous, value);
            }
        }
        
        public event Action<float, float> OnValueChange;
    }
}
