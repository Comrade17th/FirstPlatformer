using UnityEngine;

public class MobAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetupMovement(bool IsWalk)
    {
        _animator.SetBool(MobAnimatorData.Params.IsWalk, IsWalk);
    }

    public void SetAtacked()
    {
        _animator.SetTrigger(MobAnimatorData.Params.Attacked);
    }
}
