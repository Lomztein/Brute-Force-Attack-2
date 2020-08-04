using System;

namespace Lomztein.BFA2.Player.Progression
{
    public interface IUnlockList
    {
        string Name { get; }

        event Action<string, bool> OnUnlockChange;

        void Add(string identifier, bool unlocked);
        bool IsUnlocked(string identifier);
        void SetUnlocked(string identifier, bool value);
    }
}