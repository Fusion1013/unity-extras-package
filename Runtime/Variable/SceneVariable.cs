using System;
using unity_extras_package.SceneAsset;
using UnityEngine;

namespace unity_extras_package.Variable
{
    [CreateAssetMenu(fileName = "New Scene Variable", menuName = "Fusion/Variables/Scene")]
    public class SceneVariable : VariableBase<SceneAssetReference> { }
}
