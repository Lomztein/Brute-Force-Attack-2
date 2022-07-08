using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Special
{
    public class EnemyDodger : MonoBehaviour
    {
        [ModelProperty]
        public Vector2 JumpMinMax;
        [ModelProperty]
        public int JumpChanceDenominator;
        private IEnemy _enemy;

        // Start is called before the first frame update
        void Start()
        {
            _enemy = GetComponent<IEnemy>();
        }

        void FixedUpdate ()
        {
            if (Random.Range(0, JumpChanceDenominator) == 0)
            {
                DoJump();
            }
        }

        private void DoJump()
        {
            float jump = Random.Range(JumpMinMax.x, JumpMinMax.y);
            float positionAlongPath = Mathf.Min(_enemy.PathIndex + jump, _enemy.Path.Length - 1);
            Vector3 lowerPos = _enemy.Path[Mathf.Max(0, Mathf.FloorToInt(positionAlongPath))];
            Vector3 higherPos = _enemy.Path[Mathf.Min(Mathf.CeilToInt(positionAlongPath), _enemy.Path.Length - 1)];
            _enemy.PathIndex = Mathf.CeilToInt(positionAlongPath);
            transform.position = Vector3.Lerp(lowerPos, higherPos, positionAlongPath % 1f);
        }
    }
}
