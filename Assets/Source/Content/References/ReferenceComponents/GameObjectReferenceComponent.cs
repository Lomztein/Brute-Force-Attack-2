using Lomztein.BFA2.Serialization;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.ReferenceComponents
{
    public class GameObjectReferenceComponent : ReferenceComponentBase
    {
        [ModelProperty]
        public ContentPrefabReference Reference;

        protected override void Apply()
        {
            GameObject go = Reference.Instantiate();
            go.name = gameObject.name;
            go.tag = gameObject.tag;
            go.layer = gameObject.layer;

            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            go.transform.localScale = transform.localScale;
            go.transform.parent = transform.parent;

            DestroyImmediate(gameObject);
        }
    }
}
