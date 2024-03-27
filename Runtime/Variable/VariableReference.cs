using System;

namespace FusionUnityExtras.Runtime.Variable
{
    [Serializable]
    public abstract class VariableReference<T, V> where V : VariableBase<T>
    {
        public bool useConstant = true;
        public T constantValue;
        public V variable;
        
        public VariableReference() {}
        public VariableReference(T value)
        {
            useConstant = true;
            constantValue = value;
        }

        public T Value => useConstant ? constantValue : variable.Value;
    }
}
