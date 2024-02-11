using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    
    [SerializeField] private float _healthBase = 100;
    [SerializeField] private float _health = 100;
    [SerializeField] private float _baseDamage = 10;
    [SerializeField] private float _attackCoolDown = 2f;
    
    [SerializeField] private AttackController _attackController;
    [SerializeField] private ItemPicker _picker;
    
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
    }

    private void Heal(float health)
    {
        Debug.Log("Healed");
        
        _health += health;
        if (_health > _healthBase)
            _health = _healthBase;
    }
    
    private void Die()
    {
        Debug.Log("You're DEAD!");
    }
    
    private void OnEnable()
    {
        _picker.PickedMedicine += Heal;
    }

    private void OnDisable()
    {
        _picker.PickedMedicine -= Heal;
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
