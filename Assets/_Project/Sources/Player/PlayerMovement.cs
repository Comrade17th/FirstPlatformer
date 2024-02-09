using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   private const string Horizontal = nameof(Horizontal);
   private const string JumpButton = "Jump";
   
   [SerializeField] private Rigidbody2D _rb;
   [SerializeField] private float _speed = 10f;
   
   [SerializeField] private float _jumpForce = 16f;
   [SerializeField] private float _jumpCoolDown = 0.4f;
   [SerializeField] private float _coyoteTime = 0.2f;
   [SerializeField] private float _jumpBufferTime = 0.2f;
   [SerializeField] private float _jumpHighMultiplier = 0.5f;
   
   [SerializeField] private Transform _groundCheck;
   [SerializeField] private LayerMask _groundLayer;
   [SerializeField] private float _groundCheckRadius = 0.2f;
   
   private WaitForSeconds _jumpWait;
   private float _coyoteTimeCounter;
   private float _jumpBufferCounter;
   private bool _isJumping;
   
   private bool _isFacingRight = true;

   private void Start()
   {
      _jumpWait = new WaitForSeconds(_jumpCoolDown);
   }
   
   private void Update()
   {
      Move();
      Jump();
      Flip();
   }

   private void Move()
   {
      float direction = Input.GetAxis(Horizontal);
      float distance = direction * _speed * Time.deltaTime;
      var position = new Vector3(distance, 0, 0);
      
      transform.Translate(position);
   }

   private void Jump()
   {
      if (IsGrounded())
         _coyoteTimeCounter = _coyoteTime;
      else
         _coyoteTimeCounter -= Time.deltaTime;

      if (Input.GetButtonDown(JumpButton))
      {
         _jumpBufferCounter = _jumpBufferTime;
      }
      else
      {
         if(_jumpBufferCounter > 0)
            _jumpBufferCounter -= Time.deltaTime;
      }

      if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f && !_isJumping)
      {
         _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
         _jumpBufferCounter = 0f;
         
         StartCoroutine(JumpCooldown());
      }

      if (Input.GetButtonUp(JumpButton) && _rb.velocity.y > 0f)
      {
         _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _jumpHighMultiplier);
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
         Vector3 localScale = transform.localScale;
         
         _isFacingRight = !_isFacingRight;
         localScale.x *= -1f;
         transform.localScale = localScale;
      }
   }

   private IEnumerator JumpCooldown()
   {
      _isJumping = true;
      yield return _jumpWait;
      _isJumping = false;
   }
}
