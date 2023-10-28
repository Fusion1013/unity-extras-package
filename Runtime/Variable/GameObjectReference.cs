using System;
using UnityEngine;

namespace unity_extras_package.Variable
{
    [Serializable]
    public class GameObjectReference : VariableReference
    {
        public bool useConstant = true;
        public GameObject constantValue;
        public GameObjectVariable variable;
        
        public GameObjectReference() {}
        public GameObjectReference(GameObject value)
        {
            useConstant = true;
            constantValue = value;
        }

        public GameObject Value => useConstant ? constantValue : variable.Value;
        public static implicit operator GameObject(GameObjectReference reference) => reference.Value;
    }
}
