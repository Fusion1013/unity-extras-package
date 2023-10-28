using System;

namespace unity_extras_package.Variable
{
    [Serializable]
    public class StringReference : VariableReference
    {
        public bool useConstant = true;
        public string constantValue;
        public StringVariable variable;
        
        public StringReference() {}
        public StringReference(string value)
        {
            useConstant = true;
            constantValue = value;
        }

        public string Value => useConstant ? constantValue : variable.Value;
        public static implicit operator string(StringReference reference) => reference.Value;
    }
}
