using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Serializers.ModelSerializerStrategies;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers
{
    public class RootSerializer
    {
        private ValueModelSerializer _rootSerializer = new ValueModelSerializer();
        private ArrayModelSerializerStrategy _arraySerializer = new ArrayModelSerializerStrategy();

        private const string ROOT_DATA_NAME = "$Model";
        private const string ROOT_SHARED_REFS_NAME = "$Shared";

        public JToken Serialize(RootModel model)
        {
            if (model.HasSharedReferences)
            {
                return new JObject()
                {
                    { ROOT_DATA_NAME, _rootSerializer.Serialize(model.Root) },
                    { ROOT_SHARED_REFS_NAME, _arraySerializer.Serialize(model.Shared) }
                };
            }
            else
            {
                return _rootSerializer.Serialize(model.Root);
            }
        }

        public RootModel Deserialize(JToken token)
        {
            if (token is JObject obj)
            {
                if (obj.TryGetValue(ROOT_DATA_NAME, out JToken data) && obj.TryGetValue(ROOT_SHARED_REFS_NAME, out JToken shared))
                {
                    return new RootModel(_rootSerializer.Deserialize(data), _arraySerializer.Deserialize(shared) as ArrayModel);
                }
            }
            return new RootModel(_rootSerializer.Deserialize(token), new ArrayModel());
        }
    }
}
