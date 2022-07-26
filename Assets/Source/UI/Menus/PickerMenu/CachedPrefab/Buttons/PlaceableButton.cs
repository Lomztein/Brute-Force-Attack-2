using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab.Buttons
{
    public class PlaceableButton : MonoBehaviour, IPickableButton<IContentCachedPrefab>
    {
        private IContentCachedPrefab _prefab;
        private Action _onSelected;

        public Button Button;
        public Image Image;

        public string Title;
        public string Description;

        private void Awake()
        {
            Button.onClick.AddListener(() => HandlePick());
        }

        private void UpdateGraphics ()
        {
            if (Image)
            {
                Image.sprite = Iconography.GenerateSprite(_prefab.GetCache());
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
            Title = GetName(_prefab);

            SimpleToolTip toolTip = GetComponent<SimpleToolTip>();
            toolTip.Title = Title;
            toolTip.Description = GetDescription(_prefab);

            UpdateGraphics();
        }

        private string GetName (IContentCachedPrefab prefab)
        {
            var named = prefab.GetCache().GetComponent<INamed>();
            if (named != null)
            {
                return named.Name;
            }
            else
            {
                return prefab.GetCache().name;
            }
        }

        private string GetDescription(IContentCachedPrefab prefab)
        {
            if (prefab.GetCache().TryGetComponent(out INamed named))
            {
                return named.Description;
            }
            return String.Empty;
        }
    }
}
