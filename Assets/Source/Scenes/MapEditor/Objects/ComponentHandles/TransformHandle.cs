using Lomztein.BFA2.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.MapEditor.Objects.ComponentHandlers
{
    public class TransformHandle : ComponentHandleBase<Transform>
    {
        private enum State { None, Position, Rotation, Scale };
        private State _currentState;

        private float[] _transformMatrix = new float[3];
        private Vector3 TransformMatrix => new Vector3(_transformMatrix[0], _transformMatrix[1], _transformMatrix[2]);
        private bool Snap => MapEditorController.Instance.GridSnapEnabled;

        public Transform _transform;
        private Vector3 _prevPosition;
        private Vector3 _targetVector;
        private DragListener _drag;

        public override void Assign(Transform component)
        {
            _transform = component;
            _drag = DragListener.Create(DragStart, DragUpdate, DragEnd);
        }

        private void DragStart(int button, DragListener.Drag drag)
        {
        }

        private void DragUpdate(int button, DragListener.Drag drag)
        {
            Drag();
        }

        private void DragEnd(int button, DragListener.Drag drag)
        {
            Release();
        }

        public void Update()
        {
            if (_transform)
            {
                transform.position = _transform.position;
                transform.rotation = _transform.rotation;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Release()
        {
            _currentState = State.None;
            _transformMatrix = new float[3];
        }

        public void StartPosition (int axis)
        {
            _targetVector = _transform.position;
            StartDrag(axis, State.Position);
        }

        public void StartRotation(int axis)
        {
            _targetVector = _transform.rotation.eulerAngles;
            StartDrag(axis, State.Rotation);
        }

        public void StartScale (int axis)
        {
            _targetVector = _transform.localScale;
            StartDrag(axis, State.Scale);
        }

        private void StartDrag (int axis, State state)
        {
            _prevPosition = Input.WorldMousePosition;

            _transformMatrix[axis] = 1f;
            _currentState = state;
        }

        public void Drag ()
        {
            Vector3 mousePos = Input.WorldMousePosition;
            Vector3 movement = _prevPosition - mousePos;

            switch (_currentState)
            {
                case State.Position:
                    DragPosition(_transform, movement, TransformMatrix * -1f);
                    break;

                case State.Rotation:
                    DragRotation(_transform, movement, TransformMatrix);
                    break;

                case State.Scale:
                    DragScale(_transform, movement, TransformMatrix * -1f);
                    break;

                default:
                    return;
            }

            _prevPosition = mousePos;
        }

        private void DragPosition (Transform transform, Vector3 movement, Vector3 matrix)
        {
            _targetVector += transform.rotation * MultiplyVector(Quaternion.Inverse(transform.rotation) * movement, matrix);

            if (Snap)
            {
                _transform.position = new Vector3(
                    Mathf.Round(_targetVector.x / 0.5f) * 0.5f,
                    Mathf.Round(_targetVector.y / 0.5f) * 0.5f,
                    Mathf.Round(_targetVector.z / 0.5f) * 0.5f);
            }
            else
            {
                _transform.position = _targetVector;
            }
        }

        private void DragRotation(Transform transform, Vector3 movement, Vector3 matrix)
        {
            Vector3 mousePos = Input.WorldMousePosition;

            float prevAngle = Mathf.Atan2(_prevPosition.y - transform.position.y, _prevPosition.x - transform.position.x) * Mathf.Rad2Deg;
            float curAngle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;

            float delta = Mathf.DeltaAngle(prevAngle, curAngle);

            _targetVector += matrix * delta;

            if (Snap)
            {
                _transform.rotation = Quaternion.Euler(new Vector3(
                    Mathf.Round(_targetVector.x / 30f) * 30f,
                    Mathf.Round(_targetVector.y / 30f) * 30f,
                    Mathf.Round(_targetVector.z / 30f) * 30f));
            }
            else
            {
                _transform.rotation = Quaternion.Euler(_targetVector);
            }
        }

        private void DragScale(Transform transform, Vector3 movement, Vector3 matrix)
        {
            _targetVector += MultiplyVector(movement, matrix);

            if (Snap)
            {
                _transform.localScale = new Vector3(
                    Mathf.Round(_targetVector.x / 0.5f) * 0.5f,
                    Mathf.Round(_targetVector.y / 0.5f) * 0.5f,
                    Mathf.Round(_targetVector.z / 0.5f) * 0.5f);
            }
            else
            {
                _transform.localScale = _targetVector;
            }
        }

        private Vector3 MultiplyVector (Vector3 input, Vector3 matrix)
        {
            return new Vector3(
                input.x * matrix.x,
                input.y * matrix.y,
                input.z * matrix.z
                );
        }

    }
}
