using Runtime.Attributes;
using UnityEngine;

namespace Runtime.Test
{
    [AlwaysSelect]
    public class TestAttributes : MonoBehaviour
    {
        // -- Fields
        
        [ReadOnly] public float testReadOnly;
        
        // -- Methods
        
        [EditorButton]
        private void HelloWorld()
        {
            Debug.Log("Hello World");
        }
    }
}
