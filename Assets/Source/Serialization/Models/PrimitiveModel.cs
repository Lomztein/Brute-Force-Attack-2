using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models
{
    [Serializable]
    public class PrimitiveModel : ValueModel
    {
        public string Value;
        public Type StoreAs;

        public PrimitiveModel () { }

        public PrimitiveModel (object value)
        {
            if (value == null)
            {
                Value = null;
            }
            else
            {
                Value = value.ToString();

                if (value.GetType().IsEnum)
                {
                    StoreAs = typeof(string);
                }
                else
                {
                    StoreAs = value.GetType();
                }
            }
        }

        public static PrimitiveModel FromToken (JValue token)
        {
            return new PrimitiveModel()
            {
                Value = token.Value.ToString(),
                StoreAs = token.Value.GetType()
            };
        }

        public object ToObject(Type type)
        {
            if (type.IsEnum)
            {
                return Enum.Parse(type, Value);
            }
            return Convert.ChangeType(Value, type);
        }

        public T ToObject<T>() => (T)ToObject(typeof (T));

        public override string ToString()
        {
            return base.ToString() + " " + Value.ToString();
        }
    }
}
