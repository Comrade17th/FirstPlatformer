using UnityEngine;

public static class MobAnimatorData
{
    public static class Params
    {
        public static readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));
        public static readonly int Attacked = Animator.StringToHash(nameof(Attacked));
    }
}
