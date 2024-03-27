using System;
using UnityEngine;

namespace FusionUnityExtras.Runtime.Variable
{
    public abstract class VariableBase<T> : DevScriptableObject
    {
        [SerializeField] private T value;
        public T Value
        {
            get => value;
            set
            {
                var previous = this.value;
                this.value = value;
                OnValueChange?.Invoke(previous, value);
            }
        }
        
        public event Action<T, T> OnValueChange;
    }
}
