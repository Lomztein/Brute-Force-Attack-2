using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Serializers.ModelSerializerStrategies
{
    public class PathModelSerializerStrategy : ValueModelSerializerStrategyBase
    {
        public override bool CanSerialize(Type type) => type == typeof(PathModel);
        public const string VALUE_PREFIX = "$Path";

        private string ExtractPath (string input)
            => input.Substring(input.IndexOf('{'), input.IndexOf('}'));

        protected override ValueModel DeserializeWithoutMetadata(JToken token)
        {
            return new PathModel(ExtractPath(token.ToString()));
        }

        protected override JToken SerializeWithoutMetadata(ValueModel model)
        {
            PathModel pathModel = model as PathModel;
            return new JValue($"{VALUE_PREFIX}{{{pathModel.Path}}}");
        }
    }
}
