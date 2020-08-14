using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Modifiers.ModProviders
{
    public interface IModProvider
    {
        IMod Mod { get; }

        void ApplyMod();
        void RemoveMod();
    }

    public static class ModProviderExtensions
    {
        public static void Refresh (this IModProvider provider)
        {
            provider.RemoveMod();
            provider.ApplyMod();
        }
    }
}
