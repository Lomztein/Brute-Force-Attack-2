using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Buffs
{
    public abstract class EnemyBuff : ScriptableObject, IIdentifiable, INamed
    {
        [SerializeField, ModelProperty] private string _identifier;
        [SerializeField, ModelProperty] private string _name;
        [SerializeField, ModelProperty] private string _description;

        [ModelProperty]
        public float Time;
        [ModelProperty]
        public float Coeffecient = 1f;
        public Enemy Target;

        public bool Ended => Time < 0;
        private bool _timoutTriggered;

        public string Identifier => _identifier;
        public string Name => _name;
        public string Description => _description;

        public event Action<EnemyBuff> OnTimeOut;

        public int CurrentStack;
        [ModelProperty]
        public int MaxStack;

        public virtual void Begin (Enemy target, int stackSize, float time)
        {
            CurrentStack = stackSize;
            Target = target;
            Time = time;
        }

        public virtual void AddStack (int size)
        {
            CurrentStack += size;
        }

        public virtual void RemoveStack(int size)
        {
            CurrentStack -= size;
        }

        public int CanAddStack (int size) => MaxStack - (CurrentStack + size);

        public virtual void Tick (float dt)
        {
            if (Ended && !_timoutTriggered)
            {
                OnTimeOut?.Invoke(this);
                _timoutTriggered = true;
            }
        }

        public virtual void End()
        {
            CurrentStack = 0;
        }
    }
}
