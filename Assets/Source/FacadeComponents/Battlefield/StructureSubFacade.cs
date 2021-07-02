using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.StructureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.FacadeComponents.Battlefield
{
    public class StructureSubFacade : SceneFacadeSubComponent<BattlefieldFacade>
    {
        public event Action<Structure> OnStructureAdded;
        public event Action<Structure> OnStructureChanged;
        public event Action<Structure> OnStructureRemoved;

        public override void OnSceneLoaded()
        {
            StructureManager.OnStructureAdded += OnStructureAdded;
            StructureManager.OnStructureChanged += OnStructureChanged;
            StructureManager.OnStructureRemoved += OnStructureRemoved;
        }

        public override void OnSceneUnloaded()
        {
            if (StructureManager.Instance)
            {
                StructureManager.OnStructureAdded -= OnStructureAdded;
                StructureManager.OnStructureChanged -= OnStructureChanged;
                StructureManager.OnStructureRemoved -= OnStructureRemoved;
            }
        }
    }
}
