using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.DataStruct;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization
{
    public class ComponentModel : IComponentModel
    {
        public Type Type { get; private set; }
        private List<IPropertyModel> _properties = new List<IPropertyModel>();

        public void Deserialize(IDataStruct data)
        {
            Type = Type.GetType (data.GetValue<string>("TypeName"));
            IDataStruct properties = data.Get("Properties");

            foreach (IDataStruct property in properties)
            {
                _properties.Add(new PropertyModel (property.GetValue<string>("Name"), property.GetValue<string>("Value")));
            }
        }

        public object[] GetProperties() => _properties.ToArray();

        public IDataStruct Serialize()
        {
            return new JsonDataStruct(new JObject()
            {
                { "TypeName", new JValue (Type.FullName) },
                { "Properties", new JArray (_properties.Select(x => JObject.Parse (x.Serialize ().ToString ())).ToArray ()) }
            }
            );
        }

        public static ComponentModel Create (Component component)
        {
            ComponentModel model = new ComponentModel();

            Type componentType = component.GetType();
            model.Type = componentType;

            IEnumerable<FieldInfo> properties = componentType.GetFields().Where(x => x.IsDefined(typeof(ModelPropertyAttribute), true));
            foreach (FieldInfo info in properties)
            {
                model._properties.Add(new PropertyModel(info.Name, info.GetValue(component)));
            }

            return model;
        }
    }
}
