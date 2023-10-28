using System;
using unity_extras_package.SceneAsset;
using UnityEngine;

namespace unity_extras_package.Variable
{
    [Serializable]
    public class SceneReference
    {
        public bool useConstant = true;
        public SceneAssetReference constantValue;
        public SceneVariable variable;
        
        public SceneReference() {}
        public SceneReference(SceneAssetReference value)
        {
            useConstant = true;
            constantValue = value;
        }

        public SceneAssetReference Value => useConstant ? constantValue : variable.Value;
        public static implicit operator SceneAssetReference(SceneReference reference) => reference.Value;
    }
}
