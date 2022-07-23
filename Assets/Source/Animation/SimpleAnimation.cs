using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System.Collections;
using UnityEngine;

namespace Lomztein.BFA2.Animation
{
    public class SimpleAnimation : AnimationBase
    {
        [ModelProperty]
        public int HorStart = -1;
        [ModelProperty]
        public int HorEnd = -1;

        public override void Play (float animSpeed)
        {
            StopAllCoroutines();
            StartCoroutine(StartAnimation(GetAnimationDelay(GetAnimationLength(), animSpeed)));
        }

        private IEnumerator StartAnimation (float delay)
        {
            yield return Animate(SpriteSheet.GetXRange(GetHorStart(), GetHorEnd()), delay);
            ResetSprite();
        }

        private int GetHorStart() => HorStart == -1 ? 0 : HorStart;
        private int GetHorEnd() => HorEnd == -1 ? SpriteSheet.HorizontalSprites : HorEnd;
        private int GetAnimationLength() => GetHorEnd() - GetHorStart();
    }
}
