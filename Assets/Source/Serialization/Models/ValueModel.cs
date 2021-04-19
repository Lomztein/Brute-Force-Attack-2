using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models
{
    [Serializable]
    public abstract class ValueModel
    {
        [SerializeField]
        public Guid Guid = Guid.Empty;
        public string TypeName { get; private set; }
        private Type _typeCache;

        /// <summary>
        /// A property type is considered implicit if the type can be fetched from somewhere else, such as a field or array during assembly.
        /// If it cannot be fetched from somewhere else, for instance if it is a subtype, then it must be explicit.
        /// </summary>
        public bool IsTypeImplicit => string.IsNullOrEmpty(TypeName);
        public bool HasGuid => Guid != Guid.Empty;
        public bool HasMetadata => !IsTypeImplicit || HasGuid;

        public Type GetModelType ()
        {
            if (_typeCache == null)
            {
                _typeCache = ReflectionUtils.GetType(TypeName);
            }
            return _typeCache;
        }

        public ValueModel MakeImplicit()
        {
            TypeName = null;
            _typeCache = null;
            return this;
        }

        public ValueModel MakeExplicit(Type type)
        {
            TypeName = type.FullName;
            return this;
        }

        public ValueModel MakeExplicit (string typeName)
        {
            TypeName = typeName;
            return this;
        }

        public override string ToString()
        {
            return IsTypeImplicit ? "Implicit Type" : TypeName;
        }

        public static bool IsNull(ValueModel model)
            => model == null || model is NullModel;
    }
}
