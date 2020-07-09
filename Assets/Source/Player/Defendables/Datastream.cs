using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Defendables
{
    public class Datastream : Defendable
    {
        private Material _backgroundMaterial;

        public override void InstantiateEndPoints()
        {
            _backgroundMaterial = transform.Find("Background").GetComponent<Renderer>().material;

            int from = Mathf.CeilToInt(-transform.localScale.x / 2f);
            int to = Mathf.FloorToInt(transform.localScale.x / 2f);

            for (int i = from; i < to; i++)
            {
                InstantiateEndPoint(transform.position + transform.rotation * new Vector3(i + 0.5f, 0f));
            }
        }

        public override void OnHealthChanged(float before, float after, float total)
        {
            _backgroundMaterial.SetFloat("_DatastreamHealth", Mathf.Clamp01 (after / total));
        }

        public override void OnHealthExhausted()
        {
        }
    }
}