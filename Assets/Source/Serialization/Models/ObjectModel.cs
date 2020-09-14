using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.EngineComponentSerializers;
using Lomztein.BFA2.Serialization.Models.Property;
using Lomztein.BFA2.Utilities;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Models
{
    public class ObjectModel : IObjectModel
    {
        public Type Type { get; private set; }
        private List<ObjectField> _properties = new List<ObjectField>();

        public ObjectModel() { }

        public ObjectModel(Type type, params ObjectField[] properties)
        {
            Type = type;
            _properties = properties.ToList();
        }

        public ObjectModel(Type type, IObjectModel baseModel, params ObjectField[] properties) : this(type, properties)
        {
            _properties.AddRange(baseModel.GetProperties());
        }

        public ObjectField[] GetProperties() => _properties.ToArray();
    }
}
