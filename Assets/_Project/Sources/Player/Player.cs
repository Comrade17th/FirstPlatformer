using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    
    [SerializeField] private float _health = 100;
    [SerializeField] private float _baseDamage = 10;
    [SerializeField] private float _attackCoolDown = 2f;
    [SerializeField] private AttackController _attackController;
    
    private PlayerAnimator _animator;
    
    private bool _isAbleToAttack = true;
    private WaitForSeconds _attackWait;
    
    private void Start()
    {
        _attackWait = new WaitForSeconds(_attackCoolDown);
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton) && _isAbleToAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown());
        }
    }

    private void Attack()
    {
        _animator.SetAttacks();
        _attackController.Attack(_baseDamage);
        // mob.TakeDamage(_baseDamage);
    }

    private void Die()
    {
        Debug.Log("You're DEAD!");
    }

    private IEnumerator AttackCooldown()
    {
        _isAbleToAttack = false;
        yield return _attackWait;
        _isAbleToAttack = true;
    }
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
        _animator.SetAttacked();
        
        if(_health <= 0)
            Die();
    }
}
