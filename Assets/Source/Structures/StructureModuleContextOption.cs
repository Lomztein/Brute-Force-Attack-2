using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using Lomztein.BFA2.UI.ContextMenu.SubMenus;
using Lomztein.BFA2.UI.ToolTip;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Stru
{
    public class StructureModuleContextOption : MonoBehaviour, IContextMenuOptionProvider
    {
        public GameObject SubMenuPrefab;
        public ContentSpriteReference Sprite;

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            TurretAssembly assembly = GetComponent<TurretAssembly>();
            int count = GetComponent<TurretAssembly>().Modules.Count();
            if (count > 0)
                yield return new ContextMenuOption(Sprite.Get, () => UI.ContextMenu.ContextMenu.Side.Left)
                    .WithToolTip(() => SimpleToolTip.InstantiateToolTip(assembly.Modules.Count() > 0 ? "Installed modules" : "No modules installed"))
                    .WithSubMenu(InstantiateSubMenu);
        }

        private GameObject InstantiateSubMenu ()
        {
            GameObject newMenu = Instantiate(SubMenuPrefab);
            newMenu.GetComponent<StructureModulesSubMenu>().Assign(transform.GetComponent<Structure>());
            return newMenu;
        }
    }
}
