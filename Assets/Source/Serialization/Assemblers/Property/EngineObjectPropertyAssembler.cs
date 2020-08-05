using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.EngineObjectSerializers;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Assemblers.Property
{
    public class EngineObjectPropertyAssembler : IPropertyAssembler
    {
        private IEngineObjectSerializer[] _serializers = new IEngineObjectSerializer[] // TODO: find a way to automatically get all serializers from loaded assemblies.
        {
            new RectSerializer(),
            new Vector2Serializer(),
            new Vector2IntSerializer(),
            new Vector3Serializer(),
            new Vector3IntSerializer(),
            new Vector4Serializer(),
            new QuaternionSerializer(),
            new ColorSerializer(),
            new AnimationCurveSerializer(),
            new GradientSerializer(),
        };

        public object Assemble(JToken model, Type type)
        {
            return GetSerializer(type).Deserialize(model);
        }

        public JToken Dissassemble(object value, Type type)
        {
            return GetSerializer(type).Serialize(value);
        }

        private IEngineObjectSerializer GetSerializer (Type type)
        {
            return _serializers.First(x => x.CanConvert(type));
        }

        public bool Fits(Type type)
        {
            return _serializers.Any(x => x.CanConvert(type));
        }
    }
}
