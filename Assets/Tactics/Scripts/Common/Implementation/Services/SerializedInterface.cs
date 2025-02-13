using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tactics.Common.Implementation.Services
{
    [Serializable]
    public class SerializedInterface<T> where T : class
    {
        [SerializeField]
        private Object assignedObject;
        [NonSerialized]
        private T _cachedValue;

        public T Value
        {
            get
            {
                if (_cachedValue == null && assignedObject != null)
                {
                    _cachedValue = assignedObject as T;
                }

                return _cachedValue;
            }
            set
            {
                _cachedValue = value;

                if (value != null)
                {
                    assignedObject = value as Object;
                }
            }
        }
    }
}
