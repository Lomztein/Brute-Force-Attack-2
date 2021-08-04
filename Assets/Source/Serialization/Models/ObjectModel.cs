using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models
{
    [Serializable]
    public class ObjectModel : ValueModel, IEnumerable<ObjectField>
    {
        [SerializeField]
        private List<ObjectField> _properties = new List<ObjectField>();

        public ObjectModel()
        {
        }

        public ObjectModel(params ObjectField[] properties)
        {
            _properties = properties.ToList();
        }

        public void Add (string name, ValueModel value)
        {
            _properties.Add(new ObjectField(name, value));
        }

        public ObjectModel(ObjectModel baseModel, params ObjectField[] properties) : this(properties)
        {
            _properties.AddRange(baseModel.GetProperties());
        }

        public ObjectField[] GetProperties() => _properties.ToArray();

        public IEnumerator<ObjectField> GetEnumerator()
        {
            return ((IEnumerable<ObjectField>)_properties).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ObjectField>)_properties).GetEnumerator();
        }

        private ObjectField GetField(string name)
        {
            var property = GetProperties().FirstOrDefault(x => x.Name == name);
            return property;
        }

        public ValueModel GetProperty(string name)
        {
            return GetField(name)?.Model;
        }

        public T GetProperty<T>(string name) where T : ValueModel
            => (T)GetProperty(name);

        public T GetValue<T>(string name)
        {
            var field = GetField(name);
            PrimitiveModel property = field.Model as PrimitiveModel;
            return property.ToObject<T>();
        }

        public ArrayModel GetArray(string name)
            => GetProperty<ArrayModel>(name);

        public ObjectModel GetObject(string name)
            => GetProperty<ObjectModel>(name);

        public bool HasProperty(string name)
            => GetField(name) != null;
    }

    [Serializable]
    public class ObjectField
    {
        public string Name;
        [SerializeReference] public ValueModel Model;

        public ObjectField(string name, ValueModel model)
        {
            Name = name;
            Model = model;
        }

        public ObjectField()
        {
        }

        public override string ToString()
        {
            return $"{Name}: {Model}";
        }
    }
}
