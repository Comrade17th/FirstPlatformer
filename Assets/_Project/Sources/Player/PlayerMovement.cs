using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
   private const string Horizontal = nameof(Horizontal);
   private const string Vertical = nameof(Vertical);

   [SerializeField] private Transform _view;
   [SerializeField] private Rigidbody2D _rigidbody;
   [SerializeField] private float _speed = 10f;
   [SerializeField] private float _speedInAirModifier = 0.6f;
   
   [SerializeField] private float _jumpForce = 5f;
   [SerializeField] private float _jumpCoolDown = 0.4f;
   [SerializeField] private float _coyoteTime = 0.2f;
   [SerializeField] private float _jumpBufferTime = 0.2f;
   
   [SerializeField] private Transform _groundCheck;
   [SerializeField] private LayerMask _groundLayer;
   [SerializeField] private float _groundCheckRadius = 0.2f;
   
   private WaitForSeconds _jumpWait;
   private float _coyoteTimeCounter;
   private float _jumpBufferCounter;
   private bool _isJumping;
   
   private bool _isFacingRight = true;
   private PlayerAnimator _animator;

   private void Start()
   {
      _jumpWait = new WaitForSeconds(_jumpCoolDown);
      _animator = GetComponent<PlayerAnimator>();
   }
   
   private void Update()
   {
      Move();
      Jump();
      Flip();
      _animator.SetupMovement(
         _rigidbody.velocity.x,
         _rigidbody.velocity.y,
         IsGrounded());
   }

   private void Move()
   {
      float direction = Input.GetAxis(Horizontal);
      float velocityX = direction * _speed;
      
      if(IsGrounded())
         _rigidbody.velocity = new Vector2(velocityX, _rigidbody.velocity.y);
      else
         _rigidbody.velocity = new Vector2(velocityX * _speedInAirModifier, _rigidbody.velocity.y);
   }

   private void Jump()
   {
      bool isJumpButton = Input.GetAxisRaw(Vertical) > 0;
      
      if (IsGrounded())
         _coyoteTimeCounter = _coyoteTime;
      else
         _coyoteTimeCounter -= Time.deltaTime;

      if (isJumpButton)
      {
         _jumpBufferCounter = _jumpBufferTime;
      }
      else
      {
         _jumpBufferCounter -= Time.deltaTime;
      }

      if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && !_isJumping)
      {
         _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
         _jumpBufferCounter = 0f;
         
         StartCoroutine(JumpCooldown());
      }

      if (isJumpButton && _rigidbody.velocity.y > 0f)
      {
         _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
         _coyoteTimeCounter = 0f;
      }
   }

   private bool IsGrounded()
   {
      return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
   }

   private void Flip()
   {
      float direction = Input.GetAxis(Horizontal);
      
      if (_isFacingRight && direction < 0f || !_isFacingRight && direction > 0f)
      {
         Vector3 localScale = _view.localScale;
         
         _isFacingRight = !_isFacingRight;
         
         localScale.x *= -1f;
         _view.localScale = localScale;
      }
   }

   private IEnumerator JumpCooldown()
   {
      _isJumping = true;
      yield return _jumpWait;
      _isJumping = false;
   }
}
