using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.LocalizationSystem;
using Lomztein.BFA2.Scenes.Battlefield.Difficulty;
using Lomztein.BFA2.UI.Tooltip;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.MainMenu
{
    public class MapSelector : MonoBehaviour
    {
        private MapData[] _maps;

        public GameObject MapButtonPrefab;
        public Transform MapButtonParent;

        public Text MapName;
        public Text MapDescription;
        public Image Mapimage;

        private void Start()
        {
            _maps = LoadMaps();
            InstantiateMapButtons();
            SelectMap(0);
        }

        private MapData[] LoadMaps ()
        {
            return Content.GetAll("*/Maps", typeof(MapData)).Cast<MapData>().ToArray();
        }

        private void SelectMap (int index)
        {
            MapName.text = _maps[index].Name;
            MapDescription.text = _maps[index].Description;
            Mapimage.sprite = _maps[index].MapImage.Get();
            BattlefieldSettings.CurrentSettings.MapIdentifier = _maps[index].Identifier;
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
    }
}
