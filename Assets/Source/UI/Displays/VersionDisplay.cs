using Lomztein.BFA2.LocalizationSystem;
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
        Text.text = Localization.Get("MENU_VERSION", Application.version, Application.unityVersion);
    }
}
