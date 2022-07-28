using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.FacadeComponents.Battlefield
{
    public class BattlefieldFacade : SceneFacadeComponent
    {
        public BattlefieldController Battlefield;

        public StructureSubFacade Structures => Facade.GetComponent<StructureSubFacade>();
        public PlayerSubFacade Player => Facade.GetComponent<PlayerSubFacade>();
        public EnemySubFacade Enemies => Facade.GetComponent<EnemySubFacade>();
        public MasterySubFacade Mastery => Facade.GetComponent<MasterySubFacade>();

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
