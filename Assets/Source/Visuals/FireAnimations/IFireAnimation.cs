namespace Lomztein.BFA2.Visuals.FireAnimations
{
    public interface IFireAnimation
    {
        void Play(float animSpeed);

        bool IsPlaying { get; }
    }
}