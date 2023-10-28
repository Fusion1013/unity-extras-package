using System;
using unity_extras_package.SceneAsset;
using UnityEngine;

namespace unity_extras_package.Variable
{
    [CreateAssetMenu(fileName = "New Scene Variable", menuName = "Variables/Scene")]
    public class SceneVariable : DevScriptableObject
    {
        [SerializeField] private SceneAssetReference value;
        public SceneAssetReference Value
        {
            get => value;
            set
            {
                var previous = this.value;
                this.value = value;
                OnValueChange?.Invoke(previous, value);
            }
        }
        
        public event Action<SceneAssetReference, SceneAssetReference> OnValueChange;
    }
}
