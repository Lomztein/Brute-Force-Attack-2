﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Models.Turret
{
    public class TurretAssemblyModel : ITurretAssemblyModel
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ITurretComponentModel RootComponent { get; private set; }

        public TurretAssemblyModel() { }

        public TurretAssemblyModel(string name, string description, ITurretComponentModel root)
        {
            Name = name;
            Description = description;
            RootComponent = root;
        }
    }
}
