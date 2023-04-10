using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public class SellStructureContextOption : MonoBehaviour, IContextMenuOptionProvider
    {
        private const string TOOLTIP_RESOURCE = "ToolTips/SellToolTip";

        [ModelProperty]
        public ContentSpriteReference SellSprite = new ContentSpriteReference();
        public float SellValueRatio = 0.75f;
        public GameObject SubmenuPrefab;

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            return new IContextMenuOption[]
            {
                new ContextMenuOption(SellSprite.Get, () => UI.ContextMenu.ContextMenu.Side.Left, Sell).WithToolTip(GetToolTip)
            };
        }

        private GameObject GetToolTip ()
        {
            Structure structure = GetComponent<Structure>();
            IResourceCost sellValue = structure.Cost.Scale(SellValueRatio);
            GameObject toolTip = Instantiate(Resources.Load<GameObject>(TOOLTIP_RESOURCE));
            toolTip.GetComponentInChildren<CostSheetDisplay>().Display(sellValue);
            return toolTip;
        }

        private bool Sell ()
        {
            Structure structure = GetComponent<Structure>();
            IResourceCost sellValue = structure.Cost.Scale(SellValueRatio);
            foreach (var pair in sellValue.GetCost())
            {
                Player.Player.Instance.Earn(pair.Key, pair.Value, false);
            }
            Destroy(gameObject);
            return true;
        }
    }
}