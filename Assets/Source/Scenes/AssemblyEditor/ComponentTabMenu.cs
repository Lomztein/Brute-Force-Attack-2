using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Turrets;
using Lomztein.BFA2.UI.Menus;
using Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.AssemblyEditor
{
    public class ComponentTabMenu : MonoBehaviour
    {
        public TabMenu TabMenu;
        public GameObject ComponentMenuPrefab;
        public Transform ComponentMenuParent;

        private IContentCachedPrefab[] _prefabs;

        public void Start()
        {
            List<CachedPrefabPickMenu> menus = new List<CachedPrefabPickMenu>();

            _prefabs = LoadComponents();
            var groups = _prefabs.GroupBy(x => x.GetCache().GetComponent<TurretComponent>().Category);
            foreach (var group in groups)
            {
                var menuObj = Instantiate(ComponentMenuPrefab, ComponentMenuParent);
                CachedPrefabPickMenu menu = menuObj.GetComponent<CachedPrefabPickMenu>();
                menu._Name = group.Key.Name;
                menu.AddPickables(group);

                menus.Add(menu);
            }

            TabMenu.SetSubmenus(menus.ToArray(), true);
        }

        private IContentCachedPrefab[] LoadComponents()
            => ContentSystem.Content.GetAll<IContentCachedPrefab>("*/Components/*").ToArray();

        private void OnDestroy()
        {
            if (_prefabs != null)
            {
                foreach (var prefab in _prefabs)
                {
                    prefab.Dispose();
                }
            }
        }
    }
}
