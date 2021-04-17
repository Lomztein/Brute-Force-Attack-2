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
        public MapController MapController;
        private ScrollingCameraController _controller;

        public void Start()
        {
            _controller = GetComponent<ScrollingCameraController>();
        }

        private void Update ()
        {
            _controller.Limit = new Vector2(MapController.Width, MapController.Height) / 2f;
            _controller.SizeLimit = new Vector2(5f, Mathf.Max(MapController.Width, MapController.Height));
        }
    }
}
