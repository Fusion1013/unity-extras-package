using System;
using System.Collections.Generic;
using UnityEditor;

namespace unity_extras_package.Events
{
    [CustomEditor(typeof(GameEventObjectString))]
    public class GameEventObjectStringEditor : GameEventObjectBaseEditor
    {
        private string _optionalInvocationParameter;
        
        protected override void DrawOptionalParameterField()
        {
            _optionalInvocationParameter = EditorGUILayout.TextField(_optionalInvocationParameter);
        }

        protected override void InvokeEvent()
        {
            var eventObject = (GameEventObjectString)target;
            eventObject.Invoke(
                GameEventObjectBase.InvocationSource.Editor,
                _optionalInvocationParameter,
                "Invoked from the scriptable object button"
            );
        }

        protected override List<Delegate> GetDelegates()
        {
            var eventObject = (GameEventObjectString)target;
            return new List<Delegate>(eventObject.invocationList);
        }
    }
}
