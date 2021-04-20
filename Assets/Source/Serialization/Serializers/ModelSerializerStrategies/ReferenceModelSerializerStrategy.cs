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
    public class ReferenceModelSerializerStrategy : ValueModelSerializerStrategyBase
    {
        public override bool CanSerialize(Type type) => type == typeof(ReferenceModel);
        public override bool CanDeserialize(JToken token) => token is JValue value && value.ToString().StartsWith(VALUE_PREFIX);
        public const string VALUE_PREFIX = "$Ref";

        protected override ValueModel DeserializeWithoutMetadata(JToken token) => new ReferenceModel(Guid.Parse(StringUtils.ExtractContent(token.ToString(), '{', '}')));

        protected override JToken SerializeWithoutMetadata(ValueModel model)
        {
            ReferenceModel referenceModel = model as ReferenceModel;
            return new JValue($"{VALUE_PREFIX}{{{referenceModel.ReferenceId}}}");
        }
    }
}
