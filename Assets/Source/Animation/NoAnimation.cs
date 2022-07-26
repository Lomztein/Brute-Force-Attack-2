using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Animation
{
    public class NoAnimation : IAnimation
    {
        public bool IsPlaying => false;

        public void Play(float animSpeed)
        {
        }
    }
}
