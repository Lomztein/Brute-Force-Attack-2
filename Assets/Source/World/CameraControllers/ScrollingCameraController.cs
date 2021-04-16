using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2.World.CameraControllers
{
    public class ScrollingCameraController : MonoBehaviour, InputMaster.ICameraActions
    {
        public Vector2 Limit;
        public Vector2 SizeLimit;
        public float Margin;
        public float Speed;
        public float ZoomSensitivity;

        public Camera Camera;

        private float _zoom;
        private Vector2 _movement;

        private void Start()
        {
            Input.Master.Camera.SetCallbacks(this);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movement = context.ReadValue<Vector2>();
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
            _zoom = context.ReadValue<Vector2>().y;
        }

        public void Update()
        {
            Vector3 position = Mouse.current.position.ReadValue();

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

            if (Application.isFocused)
            {
                transform.Translate(new Vector3(x, y) * Speed * Time.deltaTime);
                transform.position = Clamp(transform.position);

                Camera.orthographicSize += _zoom * ZoomSensitivity * Camera.orthographicSize;
                Camera.orthographicSize = Mathf.Clamp(Camera.orthographicSize, Mathf.Max (SizeLimit.x, 10), Mathf.Max (SizeLimit.y, 10));
            }
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
