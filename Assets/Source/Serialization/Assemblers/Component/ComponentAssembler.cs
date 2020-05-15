using Lomztein.BFA2.Serialization.Assemblers.Property;
using Lomztein.BFA2.Serialization.EngineComponentSerializers;
using Lomztein.BFA2.Serialization.Models.Component;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers
{
    public class ComponentAssembler
    {

        private IEngineComponentSerializer[] _engineSerializers = new IEngineComponentSerializer[]
        {
            new TransformSerializer (),
            new CircleCollider2DSerializer(),
        };

        private IPropertyAssembler _propertyAssembler = new DefaultPropertyAssemblers();

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

            IEnumerable<FieldInfo> fields = GetModelFields(modelType);

            var properties = model.GetProperties();
            foreach (var field in fields)
            {
                var property = properties.GetProperty(field.Name);
                if (property != null)
                {
                    var value = _propertyAssembler.Assemble(property.Value, field.FieldType);
                    field.SetValue(component, value);
                }
                else
                {
                    Debug.LogWarning($"Could not find property for field {field.Name} in {modelType.Name}. Value is defualt.");
                }
            }

            var onAssembled = modelType.GetMethod("OnAssembled", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            onAssembled?.Invoke(component, Array.Empty<object>());
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

            IEnumerable<FieldInfo> fields = GetModelFields(componentType);

            foreach (FieldInfo info in fields)
            {
                try
                {
                    object componentValue = info.GetValue(component);
                    JToken value = _propertyAssembler.Dissassemble(componentValue, info.FieldType);

                    properties.Add(new PropertyModel(info.Name, value));
                }
                catch (Exception e)
                {
                    Debug.Log(componentType.Name + ": " + info.Name);
                }

            }

            return new ComponentModel(componentType, properties.ToArray());
        }

        private IEnumerable<FieldInfo> GetModelFields (Type type)
        {
            BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            List<FieldInfo> fields = new List<FieldInfo>();
            fields.AddRange(type.GetFields(_bindingFlags).Where(x => x.IsDefined(typeof(ModelPropertyAttribute), true)));
            if (type.BaseType != null)
            {
                fields.AddRange(GetModelFields(type.BaseType));
            }

            return fields;
        }
    }
}
