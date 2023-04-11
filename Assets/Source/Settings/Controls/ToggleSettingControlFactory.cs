using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Settings.Controls
{
    public class ToggleSettingControlFactory : SettingControlFactory<ToggleSetting>
    {
        private const string RESOURCE_PATH = "UI/SettingControls/ToggleSettingControl";

        public override GameObject InstantiateControl(ToggleSetting setting)
        {
            GameObject newControl = Object.Instantiate(Resources.Load<GameObject>(RESOURCE_PATH));
            newControl.transform.Find("Label").GetComponent<Text>().text = setting.Name;
            Toggle dropdown = newControl.transform.Find("Toggle").GetComponent<Toggle>();
            dropdown.isOn = setting.Get<int>() == 1;
            dropdown.onValueChanged.AddListener(x => setting.Set(x ? 1 : 0));
            Setting.OnChangedHandler onChanged = (id, value) => Setting_OnChanged(dropdown, value);
            setting.OnChanged += onChanged;
            newControl.GetComponent<NotifyOnDestroy>().Event.AddListener(() => setting.OnChanged -= onChanged);
            return newControl;
        }

        private void Setting_OnChanged(Toggle dropdown, object value)
        {
            dropdown.SetIsOnWithoutNotify((int)value == 1);
        }
    }
}
