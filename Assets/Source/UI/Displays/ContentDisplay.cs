using Lomztein.BFA2.Content;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ContentDisplay : MonoBehaviour
{
    public GameObject ContentManager;
    private IContentManager _contentManager;
    public Text Text;

    private void Awake()
    {
        _contentManager = ContentManager.GetComponent<IContentManager>();
    }

    private string FormatContent()
        => string.Join("\n", _contentManager.GetContentPacks().Select(x => x.Name));

    // Update is called once per frame
    void Update()
    {
        Text.text = FormatContent();
    }
}
