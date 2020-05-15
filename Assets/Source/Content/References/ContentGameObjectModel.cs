using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Content.References.GameObjects;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Newtonsoft.Json.Linq; 
using UnityEngine;

namespace Lomztein.BFA2.Content.References
{
    [Serializable]
    public class ContentGameObjectModel : ISerializable, IContentGameObject
    {
        public string Path;
        private IGameObjectModel _model;

        public ContentGameObjectModel() { }

        public ContentGameObjectModel(string path)
        {
            Path = path;
        }

        public ContentGameObjectModel(IGameObjectModel model)
        {
            _model = model;
        }

        public void Deserialize(JToken data)
        {
            Path = data.ToObject<string>();
        }

        public JToken Serialize()
        {
            return new JValue(Path);
        }

        private IGameObjectModel GetModel()
        {
            if (_model == null)
            {
                _model = Content.Get(Path, typeof(GameObjectModel)) as IGameObjectModel;
            }
            return _model;
        }

        public GameObject Instantiate()
        {
            IGameObjectAssembler assembler = new GameObjectAssembler();
            return assembler.Assemble(_model);
        }
    }
}
