using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.DataStruct
{
    public class JsonDataStruct : DataStructBase
    {
        private JToken _token;

        public override int Count => _token.Children().Count();
        public override bool IsNull => _token == null;

        public JsonDataStruct (JToken token)
        {
            _token = token;
        }

        public override IDataStruct Get(object identifier)
        {
            switch (identifier)
            {
                case int index:
                    var atIndex = (_token as JArray)[index];
                    return new JsonDataStruct(atIndex);

                case string key:
                    var atKey = (_token as JObject).GetValue(key);
                    return new JsonDataStruct(atKey);

                default:
                    throw new ArgumentException("Type " + identifier.GetType ().Name + " is not a valid identifier.");
            }
        }

        public override object GetValue(object identifier, Type type)
        {
            var value = _token[identifier].ToObject (type);
            return value;
        }

        public override string ToString()
        {
            return _token.ToString();
        }

        public override object ToObject(Type type)
        {
            return _token.ToObject(type);
        }
    }
}
