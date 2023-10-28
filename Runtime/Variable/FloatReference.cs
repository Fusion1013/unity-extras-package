using System;

namespace unity_extras_package.Variable
{
    [Serializable]
    public class FloatReference : VariableReference
    {
        public bool useConstant = true;
        public float constantValue;
        public FloatVariable variable;
        
        public FloatReference() {}
        public FloatReference(float value)
        {
            useConstant = true;
            constantValue = value;
        }

        public float Value => useConstant ? constantValue : variable.Value;
        public static implicit operator float(FloatReference reference) => reference.Value;
    }
}
