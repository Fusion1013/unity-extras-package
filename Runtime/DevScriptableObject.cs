using UnityEngine;

namespace FusionUnityExtras.Runtime
{
    public abstract class DevScriptableObject : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea(2, 10)] public string developerDescription = "";
#endif
    }
}
