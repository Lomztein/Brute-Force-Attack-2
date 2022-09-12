namespace Lomztein.BFA2.Weaponary.Targeting
{
    public static class TargetExtensions
    {
        public static bool ExistsAndIsValid(this ITarget target)
            => target != null && target.IsValid();
    }
}
