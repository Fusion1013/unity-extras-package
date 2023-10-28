using System;

namespace unity_extras_package.Variable
{
    [Serializable]
    public class BoolReference : VariableReference
    {
        public bool useConstant = true;
        public bool constantValue;
        public BoolVariable variable;
        
        public BoolReference() {}
        public BoolReference(bool value)
        {
            useConstant = true;
            constantValue = value;
        }

        public bool Value => useConstant ? constantValue : variable.Value;
        public static implicit operator bool(BoolReference reference) => reference.Value;
    }
}
