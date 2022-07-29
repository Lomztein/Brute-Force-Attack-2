using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets;
using System.Linq;

namespace Lomztein.BFA2.Modification.Filters
{
    public class HasModulesFilter : IModdableFilter
    {
        [ModelProperty]
        public string[] ApplicableModuleIdentifiers;
        [ModelProperty]
        public int RequiredAmount = 1;

        public bool Check(IModdable moddable)
        {
            if (moddable is TurretAssembly assembly)
            {
                int amount = assembly.Modules.Count();
                bool fit = !ApplicableModuleIdentifiers.Any() || assembly.Modules.All(x => ApplicableModuleIdentifiers.Contains(x.Item.Identifier));
                return fit && amount >= RequiredAmount;
            }
            return false;
        }
    }
}
