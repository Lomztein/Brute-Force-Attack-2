using Lomztein.BFA2.Serialization.Assemblers.Turret;
using Lomztein.BFA2.Serialization.Models.Turret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References
{
    [Serializable]
    public class ContentTurretAssemblyModel : IContentGameObject
    {
        public string Path;
        public ITurretAssemblyModel Model;

        public ContentTurretAssemblyModel() { }

        public ContentTurretAssemblyModel(string path)
        {
            Path = path;
        }

        public ContentTurretAssemblyModel (ITurretAssemblyModel model)
        {
            Model = model;
        }

        private ITurretAssemblyModel GetModel ()
        {
            if (Model == null)
            {
                Model = Content.Get(Path, typeof(ITurretAssemblyModel)) as ITurretAssemblyModel;
            }
            return Model;
        }

        public GameObject Instantiate()
        {
            GameObjectTurretAssemblyAssembler assembler = new GameObjectTurretAssemblyAssembler();
            return (assembler.Assemble(GetModel()) as Component).gameObject;
        }
    }
}
