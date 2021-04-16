using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Lomztein.BFA2.Utilities
{
    public class DragListener : MonoBehaviour
    {
        private const string RESOURCE_PATH = "Utilities/DragListener";

        private Drag[] _drags = new Drag[2];
        private Action<int, Drag> _dragStartCallback;
        private Action<int, Drag> _dragUpdateCallback;
        private Action<int, Drag> _dragEndCallback;

        public static DragListener Create (Action<int, Drag> dragStart, Action<int, Drag> dragUpdate, Action<int, Drag> dragEnd)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>(RESOURCE_PATH));
            DragListener listener = go.GetComponent<DragListener>();

            listener._dragStartCallback = dragStart;
            listener._dragUpdateCallback = dragUpdate;
            listener._dragEndCallback = dragEnd;

            return listener;
        }

        private void Start ()
        {
            Input.PrimaryClick.started += PrimaryStart;
            Input.PrimaryClick.canceled += PrimaryEnd;

            Input.SecondaryClick.started += SecondaryStart;
            Input.SecondaryClick.canceled += SecondaryEnd;
        }

        private void OnDestroy()
        {
            Input.PrimaryClick.started -= PrimaryStart;
            Input.PrimaryClick.canceled -= PrimaryEnd;

            Input.SecondaryClick.started -= SecondaryStart;
            Input.SecondaryClick.canceled -= SecondaryEnd;
        }

        private void PrimaryStart(CallbackContext context)
        {
            BeginDrag(0, context);
        }

        private void PrimaryEnd(CallbackContext context)
        {
            EndDrag(0, context);
        }

        private void SecondaryStart(CallbackContext context)
        {
            BeginDrag(1, context);
        }

        private void SecondaryEnd(CallbackContext context)
        {
            EndDrag(1, context);
        }

        private void BeginDrag(int button, CallbackContext context)
        {
            Drag drag = new Drag
            {
                ScreenStart = Input.ScreenMousePosition,
                ScreenPosition = Input.ScreenMousePosition
            };

            _drags[button] = drag;
            _dragStartCallback?.Invoke(button, drag);
        }

        private void EndDrag(int button, CallbackContext context)
        {
            _dragEndCallback?.Invoke(button, _drags[button]);
            _drags[button] = null;
        }

        private void Update()
        {
            transform.position = Input.ScreenMousePosition;
            for (int i = 0; i < _drags.Length; i++)
            {
                Drag drag = _drags[i];

                if (drag != null)
                {
                    drag.ScreenPosition = Input.ScreenMousePosition;
                    _dragUpdateCallback?.Invoke(i, drag);
                }
            }
        }

        public class Drag
        {
            public Vector3 ScreenStart { get; set; }
            public Vector3 ScreenPosition { get; set; }
            public int Button { get; set; }

            public Drag ()
            {
            }

            public Drag (Vector3 start, Vector3 end, int button)
            {
                ScreenStart = start;
                ScreenPosition = end;
                Button = button;
            }

            public Drag Sort ()
            {
                float sx = Mathf.Min(ScreenStart.x, ScreenPosition.x);
                float sy = Mathf.Min(ScreenStart.y, ScreenPosition.y);
                float sz = Mathf.Min(ScreenStart.z, ScreenPosition.z);

                float ex = Mathf.Max(ScreenStart.x, ScreenPosition.x);
                float ey = Mathf.Max(ScreenStart.y, ScreenPosition.y);
                float ez = Mathf.Max(ScreenStart.z, ScreenPosition.z);

                return new Drag(new Vector3(sx, sy, sz), new Vector3(ex, ey, ez), Button);
            }
        }
    }
}
