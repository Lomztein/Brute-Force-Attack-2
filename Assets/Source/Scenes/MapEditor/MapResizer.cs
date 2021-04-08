using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.MapEditor
{
    public class MapResizer : MonoBehaviour, IWindow
    {
        private const int MaxWidth = 512;
        private const int MaxHeight = 512;

        public InputField WidthField;
        public InputField HeightField;

        public Action<int, int> Callback;

        public event Action OnClosed;

        public void Init(int width, int height, Action<int, int> callback)
        {
            Callback = callback;

            WidthField.text = width.ToString();
            HeightField.text = height.ToString();
        }

        public void OnValueChanged ()
        {
            int.TryParse(WidthField.text, out int width);
            int.TryParse(HeightField.text, out int height);

            WidthField.text = (Mathf.RoundToInt(Mathf.Clamp(width, 2, MaxWidth) / 2f) * 2).ToString();
            HeightField.text = (Mathf.RoundToInt(Mathf.Clamp(height, 2, MaxHeight) / 2f) * 2).ToString();
        }

        public void Click ()
        {
            Callback(int.Parse(WidthField.text), int.Parse(HeightField.text));
            Close();
        }

        public void Init()
        {
        }

        public void Close()
        {
            OnClosed?.Invoke();
            Destroy(gameObject);
        }
    }
}
