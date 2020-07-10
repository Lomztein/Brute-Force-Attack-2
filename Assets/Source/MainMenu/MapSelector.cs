using Lomztein.BFA2.Battlefield;
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
        private int _current;

        public Text Text;

        private void Start()
        {
            _maps = LoadMaps();
            Cycle(0);
        }

        private MapData[] LoadMaps ()
        {
            return Content.Content.GetAll("*/Maps", typeof(MapData)).Cast<MapData>().ToArray();
        }

        private MapData GetCurrent() => _maps[_current];

        public void Cycle (int direction)
        {
            _current += direction;

            if (_current < 0)
            {
                _current = _maps.Length - 1;
            }else if (_current > _maps.Length - 1)
            {
                _current = 0;
            }

            Text.text = "Map: " + GetCurrent().Name;
            BattlefieldSettings.Map = GetCurrent();
        }
    }
}
