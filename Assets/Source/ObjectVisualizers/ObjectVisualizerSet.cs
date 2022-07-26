using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ObjectVisualizers
{
    [CreateAssetMenu(fileName = "NewObjectVisualizerSet", menuName = "BFA2/Visualizer Set")]
    public class ObjectVisualizerSet : ScriptableObject
    {
        [ModelProperty]
        public string[] VisualizerIdentifiers;

        public bool Contains(string identifier) => VisualizerIdentifiers.Contains(identifier);
    }
}
