using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Stats
{
    public class Stat : IStat
    {
        public enum Type { Additive, Multiplicative }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Identifier { get; private set; }

        private float _baseValue;
        private StatAggregate _additive = new StatAggregate(Type.Additive);
        private StatAggregate _multiplicative = new StatAggregate(Type.Multiplicative);

        private StatAggregate[] Aggregates => new[]
        {
            _additive, _multiplicative
        };

        public Stat (string identifier, string name, string description)
        {
            Identifier = identifier;
            Name = name;
            Description = description;
        }

        public float GetValue()
        {
            return (_baseValue + _additive.GetValue()) * _multiplicative.GetValue();
        }

        private StatAggregate GetAggregate (Type type)
        {
            return Aggregates.FirstOrDefault(x => x.Type == type);
        }

        public void AddElement (IStatElement element, Type type)
        {
            GetAggregate(type).AddElement(element);
        }

        public void RemoveElement (object owner, Type type)
        {
            GetAggregate(type).RemoveElement(owner);
        }

        public void Init(float baseValue)
        {
            _baseValue = baseValue;
        }
    }
}
