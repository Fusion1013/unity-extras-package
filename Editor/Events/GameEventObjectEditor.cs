using System;
using System.Collections.Generic;
using FusionUnityExtras.Events;
using UnityEditor;

namespace FusionUnityExtras.Editor.Events
{
    [CustomEditor(typeof(GameEventObject))]
    public class GameEventObjectEditor : GameEventObjectBaseEditor
    {
        protected override void DrawOptionalParameterField() { }

        protected override void InvokeEvent()
        {
            var eventObject = (GameEventObject)target;
            eventObject.Invoke(
                GameEventObjectBase.InvocationSource.Editor,
                "Invoked from the scriptable object button"
            );
        }

        protected override List<Delegate> GetDelegates()
        {
            var eventObject = (GameEventObject)target;
            return new List<Delegate>(eventObject.invocationList);
        }
    }
}
