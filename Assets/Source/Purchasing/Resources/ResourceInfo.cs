using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Purchasing.Resources
{
    // TODO: Refactor resource definition to be object-oriented, to allow much more dynamic adding.
    public enum Resource { Credits, Research, Binaries }

    public class ResourceInfo
    {
        public string Name { get; private set; }
        public string Shorthand { get; private set; }
        public string Description { get; private set; }
        public Resource Type { get; private set; }


        public ResourceInfo(Resource type, string name, string shorthand, string desc)
        {
            Type = type;
            Name = name;
            Shorthand = shorthand;
            Description = desc;
        }

        private static ResourceInfo[] _descriptors = new[]
        {
            new ResourceInfo(Resource.Credits, "Lines of Code", "LoC", "The credits used for purchasing turrets and upgrades."),
            new ResourceInfo(Resource.Research, "Research", "Research", "Research used to unlock new components as well as bonuses to existing components."),
            new ResourceInfo(Resource.Binaries, "Binaries", "Bins", "Raw binary code which can be decompiled using a Decompiler into other resources.")
        };

        public static ResourceInfo Get (Resource type)
        {
            return _descriptors.First(x => x.Type == type);
        }
    }
}
