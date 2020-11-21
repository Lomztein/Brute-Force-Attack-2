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

        public void Start()
        {
            List<CachedPrefabPickMenu> menus = new List<CachedPrefabPickMenu>();

            IContentCachedPrefab[] prefabs = LoadComponents();
            var groups = prefabs.GroupBy(x => x.GetCache().GetComponent<TurretComponent>().Category);
            foreach (var group in groups)
            {
                var menuObj = Instantiate(ComponentMenuPrefab, ComponentMenuParent);
                CachedPrefabPickMenu menu = menuObj.GetComponent<CachedPrefabPickMenu>();
                menu._Name = group.Key.Name;
                menu.AddPickables(group);

                menus.Add(menu);
            }

            TabMenu.SetSubmenus(menus.ToArray());
        }

        private IContentCachedPrefab[] LoadComponents()
            => ContentSystem.Content.GetAll<IContentCachedPrefab>("*/Components/");
    }
}
