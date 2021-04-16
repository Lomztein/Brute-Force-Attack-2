using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Editor.Content
{
    [CreateAssetMenu(fileName = ContentCompiler.CompilationTypeReferenceFileName, menuName = "BFA2/Compile Content/Compilation Type Reference")]
    public class CompilationTypeReference : ScriptableObject
    {
        public ContentCompiler.CompilationType CompilationType;
        public bool WithExplicitType;
    }
}
