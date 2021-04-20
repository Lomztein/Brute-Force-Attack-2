using Lomztein.BFA2.Assets.Source.Utilities;
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
        public override bool CanDeserialize(JToken token) => token is JValue value && value.ToString().StartsWith(PathModelSerializerStrategy.VALUE_PREFIX);

        public const string VALUE_PREFIX = "$Path";



        protected override ValueModel DeserializeWithoutMetadata(JToken token) => new PathModel(StringUtils.ExtractContent(token.ToString(), '{', '}'));

        protected override JToken SerializeWithoutMetadata(ValueModel model)
        {
            PathModel pathModel = model as PathModel;
            return new JValue($"{VALUE_PREFIX}{{{pathModel.Path}}}");
        }
    }
}
