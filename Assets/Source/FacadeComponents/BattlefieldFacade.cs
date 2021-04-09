using Lomztein.BFA2.Battlefield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.FacadeComponents
{
    public class BattlefieldFacade : SceneFacadeComponent
    {
        public BattlefieldController Battlefield;
        protected override int SceneBuildIndex => 1;

        public override void Attach(Scene scene)
        {
            Battlefield = GameObject.Find("Battlefield").GetComponent<BattlefieldController>();
        }

        public override void Detach()
        {
            Battlefield = null;
        }
    }
}
