namespace Lomztein.BFA2.Animation
{
    public interface IAnimation
    {
        void Play(float animSpeed);

        bool IsPlaying { get; }
    }
}