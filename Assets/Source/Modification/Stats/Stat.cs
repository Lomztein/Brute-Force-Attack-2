using Lomztein.BFA2.Modification.Stats.Formatters;
using Lomztein.BFA2.Modification.Stats.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Stats
{
    public class Stat : IStat
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Identifier { get; private set; }

        private float _baseValue;

        private StatAggregate _valueAggregate = new StatAggregate();

        private IStatFunction _function;
        private IStatFormatter _formatter;

        public event Action OnChanged;

        public Stat(string identifier, string name, string description, float baseValue) : this(identifier, name, description, baseValue, new MultiplicativeFunction(), new DefaultFormatter())
        {
        }

        public Stat(string identifier, string name, string description, float baseValue, IStatFunction function) : this(identifier, name, description, baseValue, function, new DefaultFormatter())
        {
        }

        public Stat(string identifier, string name, string description, float baseValue, IStatFormatter formatter) : this(identifier, name, description, baseValue, new MultiplicativeFunction(), formatter)
        {
        }

        public Stat (string identifier, string name, string description, float baseValue, IStatFunction function, IStatFormatter formatter)
        {
            Identifier = identifier;
            Name = name;
            Description = description;
            _baseValue = baseValue;
            _function = function;
            _formatter = formatter;
        }

        public float GetValue()
        {
            float value = _valueAggregate.GetValue();
            return _function.ComputeStat(_baseValue, value);
        }

        public void AddElement (IStatElement element)
        {
            _valueAggregate.AddElement(element);
            element.OnChanged += OnElementChanged;
            OnChanged?.Invoke();
        }

        private void OnElementChanged()
        {
            OnChanged?.Invoke();
        }

        public void RemoveElement (object owner)
        {
            IStatElement element = _valueAggregate.RemoveElement(owner);
            if (element != null)
            {
                element.OnChanged -= OnElementChanged;
            }
            OnChanged?.Invoke();
        }

        public override string ToString()
        {
            float aggregateValue = _valueAggregate.GetValue();
            float value = GetValue();
            return $"{Name}: {_formatter.Format(value)}";
        }
    }
}
