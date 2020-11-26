using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class VersionDisplay : MonoBehaviour
{
    public Text Text;

    void Start()
    {
        Text.text = $"Brute Force Attack 2 ver. {Application.version}\nBuilt using Unity {Application.unityVersion}";
    }
}
