using UnityEngine;
using System.Collections;
using System.Linq;
using Lomztein.BFA2.MapEditor.Objects.ComponentHandlers;
using System.Collections.Generic;

public class ComponentHandleProvider : MonoBehaviour
{
    public GameObject[] Handles;

    public IEnumerable<GameObject> GetHandles (GameObject obj)
    {
        List<GameObject> handles = new List<GameObject>();

        foreach (Component component in obj.GetComponents<Component>())
        {
            GameObject handle = Handles.FirstOrDefault(x => x.GetComponent<IComponentHandle>().CanHandle(component));

            if (handle != null)
            {
                GameObject newHandle = Instantiate(handle);
                newHandle.GetComponent<IComponentHandle>().Assign(component);
                handles.Add(newHandle);
            }
        }

        return handles;
    }
}
