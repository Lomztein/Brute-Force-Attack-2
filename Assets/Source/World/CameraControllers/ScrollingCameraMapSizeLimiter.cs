using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.World.CameraControllers
{
    public class ScrollingCameraMapSizeLimiter : MonoBehaviour
    {
        private LooseDependancy<MapController> _mapController = new LooseDependancy<MapController>();

        public void Start()
        {
            _mapController.IfExists((x) =>
            {
                    GetComponent<ScrollingCameraController>().Limit = new Vector2(x.Width, x.Height) / 2f;
                    GetComponent<ScrollingCameraController>().SizeLimit = new Vector2(5f, Mathf.Max(x.Width, x.Height));
            });
        }
    }
}
