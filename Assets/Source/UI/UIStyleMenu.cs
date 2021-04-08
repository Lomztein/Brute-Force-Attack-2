using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Game;
using Lomztein.BFA2.UI.Style;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI
{
    public class UIStyleMenu : MonoBehaviour
    {
        private const string STYLE_PATH = "*/UIStyles";

        private List<UIStyle> _styles = new List<UIStyle>();

        public GameObject StyleButtonPrefab;
        public Transform StyleButtonParent;

        private void Start()
        {
            _styles.AddRange(LoadStyles());
            GenerateStyleButtons();
        }

        public void AddStyle (UIStyle style)
        {
            _styles.Add(style);
            GenerateStyleButtons();
        }

        private IEnumerable<UIStyle> LoadStyles ()
        {
            return Content.GetAll<UIStyle>(STYLE_PATH);
        }

        private void GenerateStyleButtons ()
        {
            foreach (Transform child in StyleButtonParent)
            {
                Destroy(child.gameObject);
            }

            foreach (UIStyle style in _styles)
            {
                GameObject newButton = Instantiate(StyleButtonPrefab, StyleButtonParent);
                newButton.GetComponent<UIStyleButton>().Assign(style, OnStyleClicked);
            }
        }

        private void OnStyleClicked (UIStyle style)
        {
            PlayerProfile.CurrentProfile.Settings.UIStyle = style;
            PlayerProfile.CurrentProfile.Save();
            UIStyleController.Main.ApplyStyle(style);
        }
    }
}
