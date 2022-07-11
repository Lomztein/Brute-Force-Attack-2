using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Structures.StructureManagement
{
    public class StructureManager : MonoBehaviour
    {
        public static StructureManager Instance { get; private set; }
        private static readonly List<Structure> _structures = new List<Structure>();

        // Beware of potential event leaks.
        public static event Action<Structure> OnStructureAdded;
        public static event Action<Structure, GameObject, object> OnStructureHierarchyChanged;
        public static event Action<Structure, IStatReference, object> OnStructureStatChanged;
        public static event Action<Structure, IEventReference, object> OnStructureEventChanged;
        public static event Action<Structure> OnStructureRemoved;

        public void Awake()
        {
            Instance = this;

            if (_structures.Count != 0)
            {
                Debug.LogWarning("Structure Manager structure list not cleaned fully up between scene reloads. This implies event leak, look into it.");
            }
        }

        public static Structure[] GetStructures() => _structures.ToArray();

        public static void AddStructure (Structure structure)
        {
            _structures.Add(structure);

            structure.Destroyed += RemoveStructure;
            structure.HierarchyChanged += Structure_HierarchyChanged;
            structure.StatChanged += Structure_StatChanged;
            structure.EventChanged += Structure_EventChanged;

            OnStructureAdded?.Invoke(structure);

            Message.Send($"Placed structure '{structure.Name}'", Message.Type.Minor);
        }

        private static void Structure_EventChanged(Structure arg1, IEventReference arg2, object arg3)
        {
            OnStructureEventChanged?.Invoke(arg1, arg2, arg3);
            Debug.Log($"Structure '{arg1.Name}' event '{arg2.Event.Identifier}' changed from '{arg3}'.");
        }

        private static void Structure_StatChanged(Structure arg1, IStatReference arg2, object arg3)
        {
            OnStructureStatChanged?.Invoke(arg1, arg2, arg3);
            Debug.Log($"Structure '{arg1.Name}' stat '{arg2}' changed from '{arg3}'.");
        }

        private static void Structure_HierarchyChanged(Structure arg1, GameObject arg2, object arg3)
        {
            OnStructureHierarchyChanged?.Invoke(arg1, arg2, arg3);
            Debug.Log($"Structure '{arg1.Name}' hierarchy changed with '{arg2}' from '{arg3}'.");
        }

        public static void RemoveStructure (Structure structure)
        {
            _structures.Remove(structure);
            
            structure.Destroyed -= RemoveStructure;
            structure.HierarchyChanged -= Structure_HierarchyChanged;
            structure.StatChanged -= Structure_StatChanged;
            structure.EventChanged -= Structure_EventChanged;

            OnStructureRemoved?.Invoke(structure);

            Message.Send($"Removed structure '{structure.Name}'", Message.Type.Minor);
        }
    }
}