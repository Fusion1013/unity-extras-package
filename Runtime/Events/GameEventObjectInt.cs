using System.Collections.Generic;
using UnityEngine;

namespace FusionUnityExtras.Runtime.Events
{
    [CreateAssetMenu(fileName = "New Game Event String Object", menuName = "Fusion/Events/Game Event Int Object")]
    public class GameEventObjectInt : GameEventObjectBase
    {
        public delegate void GameEventMethodInt(int para1);
        public readonly List<GameEventMethodInt> invocationList = new();

        public void Invoke(int para1, string description = "") => Invoke(InvocationSource.Runtime, para1, description);

        public void Invoke(InvocationSource source, int para1, string description = "")
        {
            for (int i = invocationList.Count-1; i >= 0; i--) invocationList[i].Invoke(para1);

#if UNITY_EDITOR
            AddHistory(source, GetCaller(), new object[] { para1 }, description);
#endif
        }
        
        #region Subscribe / Unsubscribe

        public GameEventObjectInt Subscribe(GameEventMethodInt method)
        {
            invocationList.Add(method);
            return this;
        }
        public GameEventObjectInt Unsubscribe(GameEventMethodInt method)
        {
            invocationList.Remove(method);
            return this;
        }
        
        public static GameEventObjectInt operator +(GameEventObjectInt obj, GameEventMethodInt method) => obj.Subscribe(method);
        public static GameEventObjectInt operator -(GameEventObjectInt obj, GameEventMethodInt method) => obj.Unsubscribe(method);

        #endregion
    }
}
