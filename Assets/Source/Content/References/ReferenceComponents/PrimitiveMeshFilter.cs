using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.ReferenceComponents
{
    public class PrimitiveMeshFilter : ReferenceComponentBase
    {
        [ModelProperty]
        public PrimitiveType Type;

        protected override void Apply()
        {
            GetComponent<MeshFilter>().mesh = GenerateMesh(Type);
        }

        // Blame Unity.
        private Mesh GenerateMesh (PrimitiveType type)
        {
            GameObject meshObj = GameObject.CreatePrimitive(type);
            Mesh mesh = meshObj.GetComponent<MeshFilter>().sharedMesh;
            Destroy(meshObj);
            return mesh;
        }
    }
}
