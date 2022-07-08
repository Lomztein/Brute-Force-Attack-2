using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.Displays.Dialog
{
    public interface IWaveIntroductionPredicate
    {
        public bool ShouldShow(int forWave);
    }
}
