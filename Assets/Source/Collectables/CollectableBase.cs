using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Collectables
{
    public abstract class CollectableBase : MonoBehaviour, ICollectable
    {
        [ModelProperty]
        [SerializeField]
        public float _collectionTime;
        public float CollectionTime => _collectionTime;

        public abstract void Collect();
    }
}
