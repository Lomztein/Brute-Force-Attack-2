using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Stats.Elements
{
    public class StatSheetElement : StatSheetElementBase
    {
        public enum Behaviour { Sum, First, Range }
        public string StatIdentifier;
        public string StatName;

        public Behaviour DisplayBehaviour;

        public override bool UpdateDisplay(GameObject target)
        {
            string result = ComputeResultString(target);
            gameObject.SetActive(!string.IsNullOrEmpty(result));
            SetText(result + " " + StatName);
            return !string.IsNullOrEmpty(result);
        }

        private string ComputeResultString (GameObject target)
        {
            Structure[] structures = target.GetComponentsInChildren<Structure>();
            IEnumerable<IStatReference> references = structures.Where(x => x.Stats.HasStat(StatIdentifier)).Select(x => x.Stats.GetStat(StatIdentifier));
            if (references.Count() == 0)
            {
                return string.Empty;
            }
            else
            {
                switch (DisplayBehaviour)
                {
                    case Behaviour.Sum:
                        return references.Sum(x => x.GetValue()).ToString("0.0");

                    case Behaviour.First:
                        return references.First().GetValue().ToString("0.0");

                    case Behaviour.Range:
                        float min = references.Min(x => x.GetValue());
                        float max = references.Max(x => x.GetValue());
                        return Mathf.Approximately (min, max) ? min.ToString("0.0") : min.ToString("0.0") + " - " + max.ToString("0.0");

                    default:
                        return "n/a";
                }
            }
        }
    }
}
