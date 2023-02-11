using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Settings.Controls
{
    public class SliderSettingControlFactory : SettingControlFactory<SliderSetting>
    {
        private const string RESOURCE_PATH = "UI/SettingControls/SliderSettingControl";

        public override GameObject InstantiateControl(SliderSetting setting)
        {
            GameObject newControl = Object.Instantiate(Resources.Load<GameObject>(RESOURCE_PATH));
            newControl.transform.Find("Label").GetComponent<Text>().text = setting.Name;
            Slider slider = newControl.transform.Find("Slider").GetComponent<Slider>();
            slider.value = (float)setting.Get();
            slider.minValue = setting.MinMax.x;
            slider.maxValue = setting.MinMax.y;
            slider.wholeNumbers = setting.WholeNumbersOnly;
            slider.onValueChanged.AddListener(x => setting.Set(x));
            var text = newControl.transform.Find("Slider/Value").GetComponent<Text>();
            Setting.OnChangedHandler onChanged = (id, val) => Setting_OnChanged(setting, slider, text);
            setting.OnChanged += onChanged;
            newControl.GetComponent<NotifyOnDestroy>().Event.AddListener(() => setting.OnChanged -= onChanged);
            Setting_OnChanged(setting, slider, text);
            return newControl;
        }

        private void Setting_OnChanged(Setting obj, Slider slider, Text valueText)
        {
            valueText.text = ((float)obj.Get()).ToString((obj as SliderSetting).ValueFormat);
            slider.SetValueWithoutNotify(obj.Get<float>());
        }
    }
}
