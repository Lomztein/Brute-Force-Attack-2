using Lomztein.BFA2.Enemies.Scalers;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies
{
    public class EnemyScaleController : MonoBehaviour
    {
        public static EnemyScaleController Instance;

        private List<IEnemyScaler> _scalers = new List<IEnemyScaler>();
        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        private void Awake()
        {
            Instance = this;
            _roundController.IfExists(x =>
            {
                x.OnEnemyAdded += OnEnemySpawn;
            });
        }

        private void OnEnemySpawn(Enemy obj)
        {
            _scalers.ForEach(x => x.Scale(obj));
        }

        public void AddEnemyScalers(params IEnemyScaler[] scalers)
        {
            _scalers.AddRange(scalers);
        }

        public void AddEnemyScalers(IEnumerable<IEnemyScaler> scalers)
        {
            _scalers.AddRange(scalers);
        }
    }
}
