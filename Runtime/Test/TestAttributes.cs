using unity_extras_package.Attributes;
using UnityEngine;

namespace unity_extras_package.Test
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
