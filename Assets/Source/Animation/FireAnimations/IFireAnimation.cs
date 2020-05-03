namespace Lomztein.BFA2.Animation.FireAnimations
{
    public interface IFireAnimation
    {
        void Play(float animSpeed);

        bool IsPlaying { get; }
    }
}