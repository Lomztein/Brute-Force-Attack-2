using Lomztein.BFA2.ObjectVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lomztein.BFA2.ObjectVisualizers
{
    public abstract class ObjectVisualizerBase<T> : MonoBehaviour, IObjectVisualizer where T : class
    {
        private Transform _target;

        [SerializeField] private string _identifier;
        public string Identifier => _identifier;

        public bool CanVisualize(object obj)
        {
            return typeof (T).IsInstanceOfType(obj);
        }

        public virtual void EndVisualization()
        {
            Destroy(gameObject);
        }

        public abstract void Visualize(T component);

        public void Visualize(object obj)
        {
            T casted = obj as T;
            Assert.IsNotNull(casted);

            Visualize(casted);
            Tick(Time.fixedDeltaTime);
        }

        protected void SetPosition(Vector3 position) => transform.position = position;
        protected void SetRotation(Quaternion rotation) => transform.rotation = rotation;
        protected void Follow (Transform target)
        {
            _target = target;

            transform.position = _target.position;
            transform.rotation = _target.rotation;
        }

        public abstract void Tint(Color color);

        public virtual void Tick(float deltaTime) { }

        private void Update()
        {
            if (_target)
            {
                transform.position = _target.position;
                transform.rotation = _target.rotation;
            }
        }

        private void FixedUpdate()
        {
            Tick(Time.fixedDeltaTime);
        }
    }
}
