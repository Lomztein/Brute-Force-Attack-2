using Lomztein.BFA2.Serialization.EngineComponentSerializers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization
{
    public class ComponentAssembler
    {
        private IEngineComponentSerializer[] _engineSerializers = new IEngineComponentSerializer[]
        {
            new TransformSerializer (),
        };

        private IEngineComponentSerializer GetEngineComponentSerializer(Type type) => _engineSerializers.FirstOrDefault(x => x.Type == type);

        public void Assemble (IComponentModel model, GameObject target)
        {
            var serializer = GetEngineComponentSerializer(model.Type);
            if (serializer != null)
            {
                serializer.Deserialize(model, target);
                return;
            }

            Type modelType = model.Type;
            Component component = target.AddComponent(modelType);

            var fields = modelType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.IsDefined(typeof(ModelPropertyAttribute)));

            var properties = model.GetProperties();
            foreach (var field in fields)
            {
                var property = properties.GetProperty(field.Name);
                Type fieldType = field.FieldType;
                bool isSerializable = fieldType.GetInterfaces().Contains(typeof(ISerializable));
                object value = null;

                if (isSerializable)
                {
                    value = Activator.CreateInstance(fieldType);
                    (value as ISerializable).Deserialize(property.Value);
                }
                else
                {
                    value = property.Value.ToObject(fieldType);
                }

                field.SetValue(component, value);
            }
        }
        
        public IComponentModel Dissasemble(Component component)
        {
            var serializer = GetEngineComponentSerializer(component.GetType());
            if (serializer != null)
            {
                return serializer.Serialize(component);
            }


            Type componentType = component.GetType();
            var properties = new List<IPropertyModel>();

            IEnumerable<FieldInfo> fields = componentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.IsDefined(typeof(ModelPropertyAttribute), true));

            foreach (FieldInfo info in fields)
            {
                object componentValue = info.GetValue(component);
                JToken value = componentValue is ISerializable serializable ? serializable.Serialize() : JToken.FromObject(componentValue);

                properties.Add(new PropertyModel(info.Name, value));
            }

            return new ComponentModel(componentType, properties.ToArray());
        }
    }
}
