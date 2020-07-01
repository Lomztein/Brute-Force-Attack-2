using System;

namespace Lomztein.BFA2.UI.Windows
{
    public interface IWindow
    {
        void Init();

        void Close();
        event Action OnClosed;
    }
}