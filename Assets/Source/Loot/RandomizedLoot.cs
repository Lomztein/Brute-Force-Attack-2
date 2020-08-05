using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Loot
{
    public class RandomizedLoot
    {
        public readonly LootElementAmount[] Elements;
        public bool Empty => !Elements.Any();

        public RandomizedLoot(LootElementAmount[] elements)
        {
            Elements = elements;
        }

        public IEnumerable<GameObject> InstantiateLoot (Vector2 position, float areaScalar)
        {
            int total = Elements.Sum(x => x.Amount);
            float radius = ComputeRadius(total, areaScalar);

            List<GameObject> objs = new List<GameObject>();
            foreach (var element in Elements)
            {
                for (int i = 0; i < element.Amount; i++)
                {
                    GameObject newObj = element.Prefab.Instantiate();
                    newObj.transform.position = position + Random.insideUnitCircle * radius;
                    newObj.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
                    objs.Add(newObj);
                }
            }

            return objs;
        }

        private float ComputeRadius(int amount, float scalar)
            => Mathf.Sqrt(amount * scalar) / Mathf.PI;
    }
}
