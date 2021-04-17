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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public class MitosisModeMutator : Mutator, IHasProperties
    {
        private const string ENEMY_CONTENT_PATH = "*/Enemies";
        private IContentCachedPrefab[] _enemies;
        private IEnemy[] _enemyCache;

        private Regex _enemyTierRegex = new Regex("T[1-9]+", RegexOptions.Compiled);

        [ModelProperty]
        public int Clones;

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
            RoundController.Instance.OnEnemyKill += OnEnemyKill;
        }

        private string GetPreviousTierIdentifier (string enemyIdentifier)
        {
            Match match = _enemyTierRegex.Match(enemyIdentifier);
            if (match.Success && int.TryParse (match.Value.Substring(1), out int tierNum)) {
                if (tierNum > 1)
                {
                    return _enemyTierRegex.Replace (enemyIdentifier, $"T{tierNum - 1}");
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        private IContentCachedPrefab GetPreviousTier (string enemyIdentifier)
        {
            string prevIdentifier = GetPreviousTierIdentifier(enemyIdentifier);
            
            for (int i = 0; i < _enemies.Length; i++)
            {
                if (_enemyCache[i].UniqueIdentifier == prevIdentifier)
                {
                    return _enemies[i];
                }
            }

            return null;
        }

        private void OnEnemyKill(IEnemy obj)
        {
            IContentCachedPrefab enemy = GetPreviousTier(obj.UniqueIdentifier);
            if (enemy != null)
            {
                GameObject newEnemyGO = enemy.Instantiate();
                IEnemy newEnemy = newEnemyGO.GetComponent<IEnemy>();
                newEnemy.Init((obj as Component).transform.position + (Vector3)UnityEngine.Random.insideUnitCircle, obj.Path);
                newEnemy.PathIndex = obj.PathIndex;
            }
        }

        public void AddPropertiesTo(PropertyMenu menu)
        {
            menu.AddProperty(new NumberDefinition("Clones", Clones, true, 1, 10)).OnValueChanged += OnCloneAmountChanged;
        }

        private void OnCloneAmountChanged(object obj)
        {
            Clones = int.Parse(obj.ToString());
        }
    }
}
