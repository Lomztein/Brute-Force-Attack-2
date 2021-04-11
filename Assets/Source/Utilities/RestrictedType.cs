using UnityEngine;

namespace Util
{
    [System.Serializable]
    public class RestrictedType<T> : ISerializationCallbackReceiver where T : class
    {
        [SerializeField] private Object wrapped;

        public T Val { get; private set; }

        public static explicit operator T(RestrictedType<T> val) => val.Val;

        public void OnBeforeSerialize()
        {
            // Nothing
        }

        public void OnAfterDeserialize()
        {
            if ((object)wrapped != null) {
                Val = wrapped as T;
            } else {
                Val = null;
            }
        }
    }
}