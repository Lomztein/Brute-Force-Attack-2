using Lomztein.BFA2.AssemblyEditor;
using Lomztein.BFA2.FacadeComponents;
using Lomztein.BFA2.Structures.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.FacadeComponents
{
    public class AssemblyEditorFacade : SceneFacadeComponent
    {
        protected override int SceneBuildIndex => 2;

        public event Action<TurretAssembly, object> OnNewAssembly;
        public event Action<TurretAssembly, object> OnAssemblyCleared;
        public event Action<TurretAssembly, Tier, object> OnNewTier;

        public event Action<TurretAssembly, string> OnAssemblySaved;
        public event Action<TurretAssembly, string> OnAssemblyLoaded;

        public override void Attach(Scene scene)
        {
            AssemblyEditorController.OnAssemblyLoaded += OnAssemblyLoaded;
            AssemblyEditorController.OnAssemblySaved += OnAssemblySaved;
        }

        public override void Detach()
        {
            AssemblyEditorController.OnAssemblyLoaded -= OnAssemblyLoaded;
            AssemblyEditorController.OnAssemblySaved -= OnAssemblySaved;
        }
    }
}
