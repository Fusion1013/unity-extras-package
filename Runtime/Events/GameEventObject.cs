using System;
using System.Collections.Generic;
using UnityEngine;

namespace FusionUnityExtras.Runtime.Events
{
    [CreateAssetMenu(fileName = "New Game Event Object", menuName = "Fusion/Events/Game Event Object")]
    public class GameEventObject : GameEventObjectBase
    {
        public delegate void GameEventMethod();
        public readonly List<GameEventMethod> invocationList = new();

        public void Invoke(string description = "") => Invoke(InvocationSource.Runtime, description);
        public void Invoke(InvocationSource source, string description = "")
        {
            for (int i = invocationList.Count-1; i >= 0; i--) invocationList[i].Invoke();

#if UNITY_EDITOR
            AddHistory(source, GetCaller(), Array.Empty<object>(), description);
#endif
        }

        #region Subscribe / Unsubscribe

        public GameEventObject Subscribe(GameEventMethod method)
        {
            invocationList.Add(method);
            return this;
        }
        public GameEventObject Unsubscribe(GameEventMethod method)
        {
            invocationList.Remove(method);
            return this;
        }
        
        public static GameEventObject operator +(GameEventObject obj, GameEventMethod method) => obj.Subscribe(method);
        public static GameEventObject operator -(GameEventObject obj, GameEventMethod method) => obj.Unsubscribe(method);

        #endregion
    }
}
