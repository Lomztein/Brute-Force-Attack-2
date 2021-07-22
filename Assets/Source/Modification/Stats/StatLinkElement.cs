using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Stats
{
    public class StatLinkElement : IStatElement
    {
        public object Owner { get; private set; }
        
        private IStatReference _reference;

        public event Action OnChanged;

        public StatLinkElement (object owner, IStatReference reference)
        {
            Owner = owner;
            _reference = reference;
            _reference.OnChanged += OnStatChanged;
        }

        private void OnStatChanged()
        {
            OnChanged?.Invoke();
        }

        public float Value => _reference.GetValue();
    }
}
