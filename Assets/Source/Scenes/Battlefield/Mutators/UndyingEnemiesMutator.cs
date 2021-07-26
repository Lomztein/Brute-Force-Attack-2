using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using Lomztein.BFA2.UI.Menus.PropertyMenus.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public class UndyingEnemiesMutator : Mutator, IHasProperties
    {
        [ModelProperty]
        public float UndyingChancePercent = 25f;
        [ModelProperty]
        public bool RerollOnSuccess = false;

        private float _undyingChance => UndyingChancePercent / 100f;

        private const string ENEMY_CONTENT_PATH = "*/Enemies"; // At this point I should litteraly just make an EnemyCache class that does all this for me.
        private IContentCachedPrefab[] _enemies;
        private IEnemy[] _enemyCache;

        private void LoadEnemies()
        {
            _enemies = Content.GetAll<IContentCachedPrefab>(ENEMY_CONTENT_PATH);
            _enemyCache = new IEnemy[_enemies.Length];

            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemyCache[i] = _enemies[i].GetCache().GetComponent<IEnemy>();
            }
        }

        public override void Start()
        {
            LoadEnemies();
            RoundController.Instance.OnEnemyKilled += OnEnemyKill;
        }

        private void OnEnemyKill(IEnemy obj)
        {
            if (Roll())
            {
                SpawnClone(obj);

                if (RerollOnSuccess)
                {
                    while (Roll())
                    {
                        SpawnClone(obj);
                    }
                }
            }
        }

        private void SpawnClone(IEnemy obj)
        {
            IContentCachedPrefab prefab = null;
            for (int i = 0; i < _enemies.Length; i++)
            {
                if (_enemyCache[i].Identifier == obj.Identifier)
                {
                    prefab = _enemies[i];
                }
            }

            GameObject enemyGO = prefab.Instantiate();
            Enemy en = obj as Enemy; // flawless naming

            IEnemy enemy = enemyGO.GetComponent<IEnemy>();
            enemy.Init(en.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle, obj.Path, obj.WaveHandler);
            enemy.PathIndex = obj.PathIndex;
            enemy.WaveHandler.AddEnemy(enemy);
        }

        private bool Roll() => UnityEngine.Random.Range(0f, 1f) < _undyingChance;

        public void AddPropertiesTo(PropertyMenu menu)
        {
            menu.AddProperty(new NumberDefinition("Undying Chance %", UndyingChancePercent, false, 0, 99)).OnValueChanged += OnUndyingChanceChanged;
            menu.AddProperty(new BooleanDefinition("Reroll On Success", RerollOnSuccess)).OnValueChanged += OnRerollChanged;
        }

        private void OnRerollChanged(object obj)
        {
            RerollOnSuccess = (bool)obj;
        }

        private void OnUndyingChanceChanged(object obj)
        {
            UndyingChancePercent = float.Parse(obj.ToString());
        }
    }
}
