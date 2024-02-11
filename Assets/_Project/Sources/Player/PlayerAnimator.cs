using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetupMovement(float VelocityX, float VelocityY, bool isGrounded)
    {
        if(VelocityX == 0)
            _animator.SetBool(PlayerAnimatorData.Params.IsWalk, false);
        else
            _animator.SetBool(PlayerAnimatorData.Params.IsWalk, true);

        if (VelocityY > 0)
        {
            _animator.SetBool(PlayerAnimatorData.Params.IsJumpUp, true);
            _animator.SetBool(PlayerAnimatorData.Params.IsJumpDown, false);
        }
        else
        {
            _animator.SetBool(PlayerAnimatorData.Params.IsJumpUp, false);
            _animator.SetBool(PlayerAnimatorData.Params.IsJumpDown, true);
        }
        
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);
    }

    public void SetAttacked()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Attacked);
    }

    public void SetAttacks()
    {
        _animator.SetTrigger(PlayerAnimatorData.Params.Attacks);
    }
}
