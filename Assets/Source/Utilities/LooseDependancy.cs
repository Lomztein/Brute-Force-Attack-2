using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Utilities
{
    public class LooseDependancy<T> where T : Component
    {
        private readonly string _tag;
        public T Dependancy { get; private set; }
        public bool Exists => TryCache();

        public LooseDependancy()
        {
            _tag = typeof(T).Name;
        }

        public LooseDependancy(string tag)
        {
            _tag = tag;
        }

        private bool TryCache ()
        {
            if (Dependancy == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag(_tag);
                if (go != null)
                {
                    Dependancy = go.GetComponent<T>();
                }
            }
            return Dependancy != null;
        }

        /// <summary>
        /// Hack that uses a try-catch to check if an object technically exists, but has been destroyed. Gauranteed to slay performance
        /// if used anywhere not absolutely neccesary.
        /// </summary>
        /// <returns></returns>
        public bool IsDestroyed ()
        {
            TryCache();
            if (Dependancy == null)
            {
                try
                {
                    Dependancy.GetInstanceID();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public void IfExists (Action<T> action)
        {
            if (TryCache())
            {
                action(Dependancy);
            }
        }

        public void IfExistsOrDestroyed (Action<T> action)
        {
            if (Exists || IsDestroyed())
            {
                action(Dependancy);
            }
        }

    }
}