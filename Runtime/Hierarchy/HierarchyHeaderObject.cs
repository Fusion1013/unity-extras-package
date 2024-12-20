using UnityEngine;

namespace FusionUnityExtras.Hierarchy
{
    public class HierarchyHeaderObject : MonoBehaviour
    {
        public string title = "Header Title";
        public Color color = Color.green;
        [Space] 
        public int fontSize = 12;
        public TextAnchor textAlignment = TextAnchor.MiddleCenter;
        public FontStyle fontStyle = FontStyle.Normal;
        public Color fontColor = Color.black;
        public bool dropShadow = true;
    }
}
