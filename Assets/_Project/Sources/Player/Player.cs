using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;
    
    [SerializeField] private float _baseDamage = 10;
    [SerializeField] private float _attackCoolDown = 2f;
    [SerializeField] private Health _health;
    [SerializeField] private AttackController _attackController;
    [SerializeField] private ItemPicker _picker;
    [SerializeField] private Vamprisim _vamprisim;
    
    private PlayerAnimator _animator;
    
    private bool _isAbleToAttack = true;
    private WaitForSeconds _attackWait;

    private void Awake()
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

        if (Input.GetMouseButtonDown(RightMouseButton))
        {
            Vampirism();
        }
    }

    private void Attack()
    {
        _animator.SetAttacks();
        _attackController.Attack(_baseDamage);
    }

    private void Heal(float health)
    {
        _health.Heal(health);
    }

    private void Vampirism()
    {
        _vamprisim.DrinkBlood();
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
        _animator.SetAttacked();
        _health.TakeDamage(damage);
        
        if(_health.IsAlive == false)
            Die();
    }
}
