using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Utilities
{
    public static class UnityUtils
    {
        public static GameObject InstantiateMockGO (GameObject original)
        {
            // First create object and strip away all non-transform non-renderer components.
            GameObject model = UnityEngine.Object.Instantiate(original);

            List<Component> nonVitals = model.GetComponentsInChildren<Component>().Where(x => !(x is Transform) && !(x is Renderer) && !(x is MeshFilter)).ToList();
            foreach (Component comp in nonVitals)
            {
                UnityEngine.Object.Destroy(comp); // Might not be neccesary, test sometime.
            }

            model.SetActive(true);
            return model;
        }

        public static IEnumerator WaitForFixedSeconds(float seconds)
        {
            int frames = Mathf.RoundToInt(seconds / Time.fixedDeltaTime);
            for (int i = 0; i < frames; i++)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
