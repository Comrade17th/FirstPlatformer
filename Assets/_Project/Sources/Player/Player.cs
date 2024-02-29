using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private KeyCode AttackButton = KeyCode.B;
    [SerializeField] private KeyCode VampirismButton = KeyCode.V;
    
    [SerializeField] private float _baseDamage = 10;
    [SerializeField] private float _attackCoolDown = 2f;
    [SerializeField] private float _vampirismCoolDown = 6f;
    
    [SerializeField] private Health _health;
    [SerializeField] private AttackController _attackController;
    [SerializeField] private ItemPicker _picker;
    [SerializeField] private Vamprisim _vamprisim;
    
    private PlayerAnimator _animator;
    
    private bool _isAbleToAttack = true;
    private WaitForSeconds _attackWait;
    
    private bool _isAbleToDrinkBlood = true;
    private WaitForSeconds _vampirismWait;

    private void Awake()
    {
        _attackWait = new WaitForSeconds(_attackCoolDown);
        _vampirismWait = new WaitForSeconds(_vampirismCoolDown);
        _animator = GetComponent<PlayerAnimator>();
    }
    
    private void OnEnable()
    {
        _picker.PickedMedicine += Heal;
    }

    private void OnDisable()
    {
        _picker.PickedMedicine -= Heal;
    }

    private void Update()
    {
        if (Input.GetKeyDown(AttackButton) && _isAbleToAttack)
        {
            Attack();
            StartCoroutine(AttackCooldown());
        }

        if (Input.GetKeyDown(VampirismButton) && _isAbleToDrinkBlood)
        {
            DrinkBlood();
            StartCoroutine(VampirismCooldown());
        }
    }
    
    public void TakeDamage(float damage)
    {
        _animator.SetAttacked();
        _health.TakeDamage(damage);
        
        if(_health.IsAlive == false)
            Die();
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

    private void DrinkBlood()
    {
        _vamprisim.DrinkBlood();
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
    
    private IEnumerator VampirismCooldown()
    {
        _isAbleToDrinkBlood = false;
        yield return _vampirismWait;
        _isAbleToDrinkBlood = true;
    }
}
