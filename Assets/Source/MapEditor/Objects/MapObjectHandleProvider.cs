using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.MapEditor.Objects
{
    public class MapObjectHandleProvider : MonoBehaviour
    {
        public GameObject[] TaggedHandlers;
        public GameObject DefaultHandler;

        public MapObjectHandle GetHandle(GameObject obj)
        {
            return Instantiate(TaggedHandlers.FirstOrDefault(x => x.GetComponent<TaggedMapObjectHandle>().Tag == obj.tag) ?? DefaultHandler).GetComponent<MapObjectHandle>();
        }
    }
}
