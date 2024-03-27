using System.Collections.Generic;
using UnityEngine;

namespace FusionUnityExtras.Runtime.Events
{
    [CreateAssetMenu(fileName = "New Game Event String Object", menuName = "Fusion/Events/Game Event String Object")]
    public class GameEventObjectString : GameEventObjectBase
    {
        public delegate void GameEventMethodString(string para1);
        public readonly List<GameEventMethodString> invocationList = new();

        public void Invoke(string para1, string description = "") => Invoke(InvocationSource.Runtime, para1, description);

        public void Invoke(InvocationSource source, string para1, string description = "")
        {
            for (int i = invocationList.Count-1; i >= 0; i--) invocationList[i].Invoke(para1);

#if UNITY_EDITOR
            AddHistory(source, GetCaller(), new object[] { para1 }, description);
#endif
        }
        
        #region Subscribe / Unsubscribe

        public GameEventObjectString Subscribe(GameEventMethodString method)
        {
            invocationList.Add(method);
            return this;
        }
        public GameEventObjectString Unsubscribe(GameEventMethodString method)
        {
            invocationList.Remove(method);
            return this;
        }
        
        public static GameEventObjectString operator +(GameEventObjectString obj, GameEventMethodString method) => obj.Subscribe(method);
        public static GameEventObjectString operator -(GameEventObjectString obj, GameEventMethodString method) => obj.Unsubscribe(method);

        #endregion
    }
}
