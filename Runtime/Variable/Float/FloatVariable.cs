using System;
using Unity.VisualScripting;
using UnityEngine;

namespace unity_extras_package.Variable.Float
{
    [CreateAssetMenu(fileName = "New Float Variable", menuName = "Variables/Float")]
    public class FloatVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline] public string developerDescription = "";
#endif
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
