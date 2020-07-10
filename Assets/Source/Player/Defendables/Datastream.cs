using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Defendables
{
    public class Datastream : Defendable
    {
        private Material _backgroundMaterial;

        private void Start()
        {
            _backgroundMaterial = transform.Find("Background").GetComponent<Renderer>().material;
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