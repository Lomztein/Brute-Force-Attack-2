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

        public Transform HandleParent;

        public MapObjectHandle GetHandle(GameObject obj)
        {
            MapObjectHandle handle = Instantiate(TaggedHandlers.FirstOrDefault(x => x.GetComponent<TaggedMapObjectHandle>().Tag == obj.tag) ?? DefaultHandler).GetComponent<MapObjectHandle>();
            handle.transform.SetParent(HandleParent);
            return handle;
        }

        public void ClearHandles ()
        {
            foreach (Transform handle in HandleParent)
            {
                handle.GetComponent<MapObjectHandle>().Delete();
            }
        }
    }
}
