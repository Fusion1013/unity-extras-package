using System;

namespace unity_extras_package.Variable
{
    [Serializable]
    public class IntReference : VariableReference
    {
        public bool useConstant = true;
        public int constantValue;
        public IntVariable variable;
        
        public IntReference() {}
        public IntReference(int value)
        {
            useConstant = true;
            constantValue = value;
        }

        public int Value => useConstant ? constantValue : variable.Value;
        public static implicit operator int(IntReference reference) => reference.Value;
    }
}
