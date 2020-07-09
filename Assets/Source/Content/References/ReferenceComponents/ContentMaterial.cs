using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.ReferenceComponents
{
    public class ContentMaterial : ReferenceComponentBase
    {
        [ModelProperty]
        public ContentMaterialReference Reference;

        protected override void Apply()
        {
            GetComponent<Renderer>().material = Reference.GetMaterial();
        }
    }
}
