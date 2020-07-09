using Lomztein.BFA2.Player.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Defendables
{
    public abstract class Defendable : MonoBehaviour
    {
        private static string EndPointPath = "Prefabs/EnemyEndPoint";
        private static string EndPointParent = "EndPoints";

        private IHealthContainer _healthContainer;

        public void OnMapObjectAssembled ()
        {
            _healthContainer = GetComponent<IHealthContainer>();

            _healthContainer.OnHealthChanged += OnHealthChanged;
            _healthContainer.OnHealthExhausted += OnHealthExhausted;

            InstantiateEndPoints();
        }

        public abstract void InstantiateEndPoints();

        public abstract void OnHealthChanged(float before, float after, float total);

        public abstract void OnHealthExhausted();

        private Transform GetEndPointParent() => transform.Find(EndPointParent);

        protected GameObject InstantiateEndPoint(Vector3 position) => Instantiate(Resources.Load<GameObject>(EndPointPath), position, Quaternion.identity, GetEndPointParent());
    }
}
