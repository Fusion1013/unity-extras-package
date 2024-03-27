using FusionUnityExtras.Runtime.SceneAsset;
using UnityEngine;

namespace FusionUnityExtras.Runtime.Variable
{
    [CreateAssetMenu(fileName = "New Scene Variable", menuName = "Fusion/Variables/Scene")]
    public class SceneVariable : VariableBase<SceneAssetReference> { }
}
