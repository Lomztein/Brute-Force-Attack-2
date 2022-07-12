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
        public bool GatherFromRoot;
        public bool IncludeInactive;
        public string Format = "0.0";

        public override bool UpdateDisplay(GameObject target)
        {
            string result = ComputeResultString(target);
            gameObject.SetActive(!string.IsNullOrEmpty(result));
            SetText(result + " " + StatName);
            return !string.IsNullOrEmpty(result);
        }

        private string ComputeResultString (GameObject target)
        {
            Structure[] structures = GatherFromRoot ? target.transform.root.GetComponentsInChildren<Structure>(IncludeInactive) : target.GetComponentsInChildren<Structure>(IncludeInactive);
            List<IStatReference> references = structures.Where(x => x.Stats.HasStat(StatIdentifier)).Select(x => x.Stats.GetStat(StatIdentifier)).ToList();
            if (references.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                switch (DisplayBehaviour)
                {
                    case Behaviour.Sum:
                        return references.Sum(x => x.GetValue()).ToString(Format);

                    case Behaviour.First:
                        return references.First().GetValue().ToString(Format);

                    case Behaviour.Range:
                        float min = references.Min(x => x.GetValue());
                        float max = references.Max(x => x.GetValue());
                        return Mathf.Approximately (min, max) ? min.ToString(Format) : min.ToString(Format) + " - " + max.ToString(Format);

                    default:
                        return "n/a";
                }
            }
        }
    }
}
