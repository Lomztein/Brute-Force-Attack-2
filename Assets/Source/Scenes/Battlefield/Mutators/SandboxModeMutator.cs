using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lomztein.BFA2.Mutators
{
    public class SandboxModeMutator : Mutator
    {
        public override void Start()
        {
            Object.Destroy(GameObject.Find("Player"));
            Content.Get<IContentCachedPrefab>("Core/Misc/SandboxPlayer.json").Instantiate();
        }
    }
}
