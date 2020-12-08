using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Paths
{
    public static string StreamingAssets => Application.streamingAssetsPath + "/";
    public static string Data => Application.dataPath + "/";
    public static string PersistantData => Application.persistentDataPath + "/";
}
