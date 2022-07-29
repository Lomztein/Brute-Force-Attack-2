using Lomztein.BFA2.AssemblyEditor;
using Lomztein.BFA2.FacadeComponents;
using Lomztein.BFA2.MapEditor;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.FacadeComponents
{
    public class MapEditorFacade : SceneFacadeComponent
    {
        protected override int SceneBuildIndex => 3;

        public event Action<MapData, object> OnNewMap;
        public event Action<MapData, object> OnMapResized;

        public event Action<MapData, string> OnMapSaved;
        public event Action<MapData, string> OnMapLoaded;

        public override void Attach(Scene scene)
        {
            MapEditorController.Instance.OnMapLoaded += OnMapLoaded;
            MapEditorController.Instance.OnMapSaved += OnMapSaved;
        }

        public override void Detach()
        {
            if (MapEditorController.Instance)
            {
                MapEditorController.Instance.OnMapLoaded -= OnMapLoaded;
                MapEditorController.Instance.OnMapSaved -= OnMapSaved;
            }
        }
    }
}
