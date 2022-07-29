using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.StructureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.FacadeComponents.Battlefield
{
    public class StructureSubFacade : SceneFacadeSubComponent<BattlefieldFacade>
    {
        public event Action<Structure, object> OnStructureAdded;
        public event Action<Structure, GameObject, object> OnStructureHierarchyChanged;
        public event Action<Structure, IStatReference, object> OnStructureStatChanged;
        public event Action<Structure, IEventReference, object> OnStructureEventChanged;
        public event Action<Structure, IEvent, object> OnStructureEventExecuted;
        public event Action<Structure> OnStructureRemoved;

        public override void OnSceneLoaded()
        {
            StructureManager.OnStructureAdded += OnStructureAdded;
            StructureManager.OnStructureHierarchyChanged += OnStructureHierarchyChanged;
            StructureManager.OnStructureStatChanged += OnStructureStatChanged;
            StructureManager.OnStructureEventChanged += OnStructureEventChanged;
            StructureManager.OnStructureEventExecuted += OnStructureEventExecuted;
            StructureManager.OnStructureRemoved += OnStructureRemoved;
        }

        public override void OnSceneUnloaded()
        {
            if (StructureManager.Instance)
            {
                StructureManager.OnStructureAdded -= OnStructureAdded;
                StructureManager.OnStructureHierarchyChanged -= OnStructureHierarchyChanged;
                StructureManager.OnStructureStatChanged -= OnStructureStatChanged;
                StructureManager.OnStructureEventChanged -= OnStructureEventChanged;
                StructureManager.OnStructureEventExecuted -= OnStructureEventExecuted;
                StructureManager.OnStructureRemoved -= OnStructureRemoved;
            }
        }
    }
}
