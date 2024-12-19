using UnityEngine;

namespace FusionUnityExtras.Variable
{
    public class AnimatorIntSetter : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        [Tooltip("Variable to read from and send to the Animator as the specified parameter")]
        private IntVariable variable;

        [SerializeField]
        [Tooltip("Name of the parameter to set with the value of variable")]
        private string parameterName;
        
        private UnityEngine.Animator _animator;

        #endregion

        private void Awake() => _animator = GetComponent<UnityEngine.Animator>();
        private void OnEnable() => variable.OnValueChange += SetValue;
        private void OnDisable() => variable.OnValueChange += SetValue;
        private void SetValue(int previous, int current) => _animator.SetInteger(parameterName, variable.Value);
    }
}
