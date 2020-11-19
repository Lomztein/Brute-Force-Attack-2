using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PolymorphicSerializationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.streamingAssetsPath + "/Test/PolymorphicSerializationTest.json";
        //FruitContainer container = new FruitContainer();
        //var json = ObjectPipeline.UnbuildObject(container);
        //File.WriteAllText(path, json.ToString());

        string text = File.ReadAllText(path);
        FruitContainer loaded = ObjectPipeline.BuildObject<FruitContainer>(JToken.Parse(text));
    }

    public class FruitContainer
    {
        [ModelProperty]
        public Fruit One = new Orange();
        [ModelProperty]
        public Fruit Two = new Pear();

        [ModelProperty]
        public Fruit[] Fruits = new Fruit[]
        {
            new Orange (),
            new Pear ()
        };
    }

    public abstract class Fruit
    {
        [ModelProperty]
        public float Eatability;
    }

    public class Orange : Fruit
    {
        [ModelProperty]
        public bool Peeled;
    }

    public class Pear : Fruit
    {
        [ModelProperty]
        public float Sweetness;
    }
}
