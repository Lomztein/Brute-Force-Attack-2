using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.LocalizationSystem;
using Lomztein.BFA2.Scenes.Battlefield;
using Lomztein.BFA2.Scenes.Battlefield.Difficulty;
using Lomztein.BFA2.UI.ToolTip;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class MapSelector : MonoBehaviour, ICustomGameAspectController
    {
        private MapData[] _maps;

        public GameObject MapButtonPrefab;
        public Transform MapButtonParent;

        public Text MapName;
        public Text MapDescription;
        public Image MapPreview;
        public Text PreviewMissingText;

        private void Awake()
        {
            _maps = LoadMaps();
            InstantiateMapButtons();
        }

        private MapData[] LoadMaps ()
        {
            List<MapData> maps = Content.GetAll("*/Maps", typeof(MapData), false).Cast<MapData>().ToList();
            maps.Sort((x, y) => (x.Width * x.Height) - (y.Width * y.Height));
            return maps.ToArray();
        }

        private void SelectMap (int index)
        {
            MapName.text = _maps[index].Name;
            MapDescription.text = _maps[index].Description;
            SetMapPreview(index);

            BattlefieldInitializeInfo.NewSettings.MapIdentifier = _maps[index].Identifier;
        }

        private void SetMapPreview(int index)
        {
            Texture2D preview = _maps[index].Preview;
            if (preview)
            {
                MapPreview.sprite = Sprite.Create(preview, new Rect(0, 0, preview.width, preview.height), Vector2.one / 2f);
                MapPreview.gameObject.SetActive(true);
                PreviewMissingText.gameObject.SetActive(false);
            }
            else
            {
                MapPreview.gameObject.SetActive(false);
                PreviewMissingText.gameObject.SetActive(true);
            }
        }

        public void InstantiateMapButtons ()
        {
            foreach (Transform child in MapButtonParent)
            {
                Destroy(child);
            }

            for (int i = 0; i < _maps.Length; i++)
            {
                MapData data = _maps[i];
                GameObject newButton = Instantiate(MapButtonPrefab, MapButtonParent);
                newButton.GetComponentInChildren<Text>().text = data.Name;
                Button button = newButton.GetComponentInChildren<Button>();
                AddListener(button, i);
            }
        }

        private void AddListener(Button button, int index)
        {
            button.onClick.AddListener(() => SelectMap(index));
        }

        public void ApplyBattlefieldSettings(BattlefieldSettings settings)
        {
            List<MapData> list = _maps.ToList();
            MapData data = list.First(x => x.Identifier == settings.MapIdentifier);
            SelectMap(list.IndexOf(data));
        }
    }
}
