using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.ItemMenu
{
    public class ItemPickButton : MonoBehaviour, IPickableButton<Item>, ITooltip
    {
        public Button Button;
        public Image Sprite;

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Footnote { get; private set; }

        public void Assign(Item pickable, Action onPickedCallback)
        {
            Button.onClick.AddListener(() => onPickedCallback());
            Sprite.sprite = pickable.Sprite.Get();
            Sprite.color = pickable.SpriteTint;

            Title = pickable.Name;
            Description = pickable.Description;
        }
    }
}
