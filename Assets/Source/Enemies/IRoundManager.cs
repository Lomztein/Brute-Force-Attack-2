using System;

namespace Lomztein.BFA2.Enemies
{
    public interface IRoundManager
    {
        void InvokeDelayed(Action callback, float time);
    }
}