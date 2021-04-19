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
        public const string VALUE_PREFIX = "$Ref";

        private string Extract(string input)
            => input.Substring(input.IndexOf('{'), input.IndexOf('}'));

        protected override ValueModel DeserializeWithoutMetadata(JToken token)
        {
            return new ReferenceModel(Guid.Parse(Extract(token.ToString())));
        }

        protected override JToken SerializeWithoutMetadata(ValueModel model)
        {
            ReferenceModel referenceModel = model as ReferenceModel;
            return new JValue($"{VALUE_PREFIX}{{{referenceModel.ReferenceId}}}");
        }
    }
}
