namespace Lomztein.BFA2.Enemies.Waves
{
    public interface IWaveCollection
    {
        string Identifier { get; }

        IWave GetWave(int index);
    }
}