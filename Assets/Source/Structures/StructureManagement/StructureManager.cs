using Lomztein.BFA2.Player.Messages;
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
        public static event Action<Structure> OnStructureChanged;
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
            structure.Changed += StructureChanged;

            OnStructureAdded?.Invoke(structure);

            Message.Send($"Placed structure '{structure.Name}'", Message.Type.Minor);
        }

        public static void RemoveStructure (Structure structure)
        {
            _structures.Remove(structure);
            
            structure.Destroyed -= RemoveStructure;
            structure.Changed -= StructureChanged;

            OnStructureRemoved?.Invoke(structure);

            Message.Send($"Removed structure '{structure.Name}'", Message.Type.Minor);
        }

        private static void StructureChanged (Structure structure)
        {
            OnStructureChanged?.Invoke(structure);

            Debug.Log($"Structure '{structure.Name}' has changed.");
        }
    }
}