using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.FacadeComponents.Battlefield
{
    public class PlayerSubFacade : SceneFacadeSubComponent<BattlefieldFacade>
    {
        public event Action<Resource, int, int> OnResourceChanged;

        public event Action<float, float, float, object> OnHealthChanged;
        public event Action<object> OnHealthExhausted;

        public event Action<string, bool> OnUnlockChanged;

        public override void OnSceneLoaded()
        {
            Player.Player.Resources.OnResourceChanged += OnResourceChanged;
            Player.Player.Health.OnHealthChanged += OnHealthChanged;
            Player.Player.Health.OnHealthExhausted += OnHealthExhausted;
            Player.Player.Unlocks.OnUnlockChange += OnUnlockChanged;
        }

        public override void OnSceneUnloaded()
        {
            if (Player.Player.Instance)
            {
                Player.Player.Resources.OnResourceChanged -= OnResourceChanged;
                Player.Player.Health.OnHealthChanged -= OnHealthChanged;
                Player.Player.Health.OnHealthExhausted -= OnHealthExhausted;
                Player.Player.Unlocks.OnUnlockChange -= OnUnlockChanged;
            }
        }
    }
}
