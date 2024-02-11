using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int IsJumpUp = Animator.StringToHash(nameof(IsJumpUp));
        public static readonly int IsJumpDown = Animator.StringToHash(nameof(IsJumpDown));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
        public static readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));
        public static readonly int Attacked = Animator.StringToHash(nameof(Attacked));
        public static readonly int Attacks = Animator.StringToHash(nameof(Attacks));
    }
}
