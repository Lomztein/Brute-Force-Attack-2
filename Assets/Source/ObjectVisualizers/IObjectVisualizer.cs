using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ObjectVisualizers
{
    public interface IObjectVisualizer : IIdentifiable
    {
        bool CanVisualize(object obj);

        void Visualize(object obj);

        void EndVisualization();

        void Tint(Color color);
    }
}
