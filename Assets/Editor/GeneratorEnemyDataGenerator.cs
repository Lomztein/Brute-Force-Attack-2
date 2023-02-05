using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Lomztein.BFA2;
using System.Text.RegularExpressions;
using System;
using System.IO;

public class GeneratorEnemyDataGenerator : EditorWindow
{
    public GeneratorEnemyData BaseData;
    public int NumTiers;
    public int NumWavesBetweenTiers = 13;
    public int NumWavesPresent = 15;

    private static readonly Regex TierRegex = new Regex(@"(T\d)");
    private static readonly Regex PrefixRegex = new Regex(@"(.*\.)");

    [MenuItem("BFA2/Generator Enemy Data Generator")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        GeneratorEnemyDataGenerator window = (GeneratorEnemyDataGenerator)GetWindow(typeof(GeneratorEnemyDataGenerator));
        window.Show();
    }

    private void OnGUI()
    {
        BaseData = EditorGUILayout.ObjectField("Base", BaseData, typeof(GeneratorEnemyData), false) as GeneratorEnemyData;
        NumTiers = EditorGUILayout.IntField("Tiers", NumTiers);
        NumWavesBetweenTiers = EditorGUILayout.IntField("Waves Between Tiers", NumWavesBetweenTiers);
        NumWavesPresent = EditorGUILayout.IntField("Waves Present", NumWavesPresent);
        if (GUILayout.Button("Generate!"))
        {
            Generate(BaseData, NumTiers, NumWavesBetweenTiers, NumWavesPresent);
        }
    }

    private void Generate (GeneratorEnemyData baseData, int numTiers, int numWavesBetweenTiers, int numWavesPresent) 
    {
        string basePath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(baseData));
        Debug.Log(basePath);

        string baseName = TierRegex.Replace(baseData.EnemyIdentifier, "");
        int earliestWave = baseData.EarliestWave;
        for (int tier = 2; tier <= numTiers; tier++)
        {
            earliestWave += numWavesBetweenTiers;

            string tierName = $"{baseName}T{tier}";
            double tierDifficultyValue = baseData.DifficultyValue * Math.Pow(10, tier - 1);
            int tierLastWave = earliestWave + NumWavesPresent;

            GeneratorEnemyData newData = CreateInstance<GeneratorEnemyData>();
            newData.name = PrefixRegex.Replace(tierName, "");
            newData.EnemyIdentifier = tierName;
            newData.DifficultyValue = tierDifficultyValue;
            newData.EarliestWave = earliestWave;
            newData.LastWave = tierLastWave;

            string tierPath = Path.Combine(basePath, newData.name + ".asset");
            AssetDatabase.DeleteAsset(tierPath);
            AssetDatabase.CreateAsset(newData, tierPath);
        }
    }
}