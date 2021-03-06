﻿using Lomztein.BFA2.UI.Menus.PropertyMenus.Controls;
using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PropertyMenus
{
    public class PropertyMenu : MonoBehaviour
    {
        public GameObject[] ControlPrefabs;
        public Transform ControlParent;

        public event Action OnPropertyChanged;

        public void Clear ()
        {
            foreach (Transform child in ControlParent)
            {
                Destroy(child.gameObject);
            }
        }

        public PropertyControl AddProperty(PropertyDefinition def)
        {
            return InstantiateControl(def);
        }

        private PropertyControl InstantiateControl (PropertyDefinition def)
        {
            GameObject prefab = SelectControl(def);

            if (prefab)
            {
                GameObject newObject = Instantiate(prefab, ControlParent);
                PropertyControl control = newObject.GetComponent<PropertyControl>();
                control.Control(def);

                control.OnValueChanged += OnPropertyValueChanged; // Will doing this result in control dangling if destroyed, or will it be cleaned up by GC?
                
                return control;
            }

            throw new InvalidOperationException($"There are no controls that can take care of property def type {def.GetType().Name}'.");
        }

        private void OnPropertyValueChanged(object obj)
        {
            OnPropertyChanged?.Invoke();
        }

        private GameObject SelectControl (PropertyDefinition def)
        {
            foreach (GameObject go in ControlPrefabs)
            {
                PropertyControl control = go.GetComponent<PropertyControl>();
                if (control.CanControl(def))
                {
                    return go;
                }
            }

            return null;
        }
    }
}