using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Research.UI
{
    public class ResearchProgressTracker : ResearchTracker
    {
        public override float Progress => _option.TimePayed / (float)_option.TimeCost;
        public override string Status => $"{_option.TimeCost - _option.TimePayed} waves remaining";
    }
}
