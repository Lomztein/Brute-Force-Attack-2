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
    public class TurretSeller : MonoBehaviour, IContextMenuOptionProvider
    {
        [ModelProperty]
        public ContentSpriteReference SellSprite = new ContentSpriteReference();
        public float SellValueRatio = 0.75f;

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            return new IContextMenuOption[]
            {
                new ContextMenuOption(() => "Sell", GetSellValueString, SellSprite.Get, () => null, () => UI.ContextMenu.ContextMenu.Side.Left, Sell, () => true)
            };
        }

        private string GetSellValueString ()
        {
            Structure structure = GetComponent<Structure>();
            IResourceCost sellValue = structure.Cost.Scale(SellValueRatio);
            return sellValue.Format();
        }

        private bool Sell ()
        {
            Structure structure = GetComponent<Structure>();
            IResourceCost sellValue = structure.Cost.Scale(SellValueRatio);
            foreach (var pair in sellValue.GetCost())
            {
                Player.Player.Instance.Earn(pair.Key, pair.Value);
            }
            Destroy(gameObject);
            return true;
        }
    }
}