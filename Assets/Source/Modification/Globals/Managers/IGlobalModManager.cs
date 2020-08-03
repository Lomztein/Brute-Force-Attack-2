using System;

namespace Lomztein.BFA2.Modification.Globals
{
    public interface IGlobalModManager
    {
        bool Fits(string identifier);
        void RemoveMod(GlobalMod mod);
        void TakeMod(GlobalMod mod);
    }
}