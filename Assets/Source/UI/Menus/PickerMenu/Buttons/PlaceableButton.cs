using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.Buttons
{
    public class PlaceableButton : MonoBehaviour, IPickableButton, ITooltip
    {
        private IContentCachedPrefab _prefab;
        private Action _onSelected;

        public Button Button;
        public Image Image;

        public string Title { get; private set; }
        public string Description => string.Empty;
        public string Footnote => string.Empty;

        private void Awake()
        {
            Button.onClick.AddListener(() => HandlePick());
        }

        private void UpdateGraphics ()
        {
            if (Image)
            {
                Texture2D tex = Iconography.GenerateIcon(_prefab.GetCache());
                Image.sprite = Sprite.Create (tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
        }

        public void HandlePick()
        {
            _onSelected();
        }

        public void Assign(IContentCachedPrefab prefab, Action onSelected)
        {
            _prefab = prefab;
            _onSelected = onSelected;
            Title = prefab.GetCache().name;
            UpdateGraphics();
        }
    }
}
