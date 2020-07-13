using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.World.CameraControllers
{
    public class ScrollingCameraController : MonoBehaviour
    {
        public Vector2 Limit;
        public float Margin;
        public float Speed;

        public void Update()
        {
            Vector3 position = Input.mousePosition;

            float x = 0f;
            float y = 0f;

            if (position.x < 0f + Margin)
            {
                x = -1f;
            }

            if (position.y < 0f + Margin)
            {
                y = -1f;
            }

            if (position.x > Screen.width - Margin)
            {
                x = 1f;
            }

            if (position.y > Screen.height - Margin)
            {
                y = 1f;
            }

            transform.Translate(new Vector3(x, y) * Speed * Time.deltaTime);
            transform.position = Clamp(transform.position);
        }

        private Vector3 Clamp (Vector3 position)
        {
            return new Vector3(
                Mathf.Clamp(position.x, -Limit.x, Limit.x),
                Mathf.Clamp(position.y, -Limit.y, Limit.y),
                position.z
                );
        }
    }
}
