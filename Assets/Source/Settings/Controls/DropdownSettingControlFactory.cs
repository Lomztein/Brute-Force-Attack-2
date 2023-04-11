using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Settings.Controls
{
    public class DropdownSettingControlFactory : SettingControlFactory<DropdownSetting>
    {
        private const string RESOURCE_PATH = "UI/SettingControls/DropdownSettingControl";

        public override GameObject InstantiateControl(DropdownSetting setting)
        {
            GameObject newControl = Object.Instantiate(Resources.Load<GameObject>(RESOURCE_PATH));
            newControl.transform.Find("Label").GetComponent<Text>().text = setting.Name;
            Dropdown dropdown = newControl.transform.Find("Dropdown").GetComponent<Dropdown>();
            dropdown.options = setting.Options.Select(x => new Dropdown.OptionData(x)).ToList();
            dropdown.value = setting.Get<int>();
            dropdown.onValueChanged.AddListener(x => setting.Set(x));
            Setting.OnChangedHandler onChanged = (id, value) => Setting_OnChanged(dropdown, value);
            setting.OnChanged += onChanged;
            newControl.GetComponent<NotifyOnDestroy>().Event.AddListener(() => setting.OnChanged -= onChanged);
            return newControl;
        }

        private void Setting_OnChanged(Dropdown dropdown, object value)
        {
            dropdown.SetValueWithoutNotify((int)value);
        }
    }
}
