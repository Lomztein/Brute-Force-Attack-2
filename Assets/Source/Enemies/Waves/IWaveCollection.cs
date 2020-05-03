namespace Lomztein.BFA2.Enemies.Waves
{
    public interface IWaveCollection
    {
        void Init(IWave[] waves);
        IWave GetWave(int index);
    }
}