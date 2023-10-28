using System;
using UnityEngine;

namespace unity_extras_package.Variable
{
    [CreateAssetMenu(fileName = "New String Variable", menuName = "Variables/String")]
    public class StringVariable : DevScriptableObject
    {
        [SerializeField] private string value;
        public string Value
        {
            get => value;
            set
            {
                var previous = this.value;
                this.value = value;
                OnValueChange?.Invoke(previous, value);
            }
        }
        
        public event Action<string, string> OnValueChange;
    }
}
