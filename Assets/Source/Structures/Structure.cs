using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets;
using Lomztein.BFA2.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures
{
    public class Structure : MonoBehaviour, IPurchasable, IGridObject
    {
        [SerializeField]
        [ModelProperty]
        private string _name;
        public string Name { get => _name; set => _name = value; }

        [SerializeField]
        [ModelProperty]
        private string _description;
        public string Description { get => _description; set => _description = value; }
        public IResourceCost Cost => _cost;
        [SerializeField]
        [ModelProperty]
        private ResourceCost _cost;

        public Sprite Sprite => Iconography.GenerateSprite(gameObject);

        [SerializeField]
        [ModelProperty]
        private Size _width;
        public Size Width => _width;
        [SerializeField]
        [ModelProperty]
        private Size _height;
        public Size Height => _height;
    }
}