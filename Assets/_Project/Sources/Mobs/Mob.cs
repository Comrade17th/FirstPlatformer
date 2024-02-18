using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(MobAnimator))]
[RequireComponent(typeof(Wanderer))]
[RequireComponent(typeof(Health))]
public class Mob : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private Wanderer _wanderer;
    [SerializeField] private Health _health;
    [SerializeField] private float _baseDamage = 10;
    [SerializeField] private float _attackCoolDown = 2f;
    [SerializeField] private float _timeToDie = 1f;
    
    private MobAnimator _animator;
    private MobSpawner _spawner;
    
    private int _spawnId;
    private WaitForSeconds _waitDie;
    private WaitForSeconds _waitAttack;
    private bool _isAbleToAttack = true;

    public bool IsAlive => _health.IsAlive;
    
    public event Action Dead;

    private void Awake()
    {
        _waitAttack = new WaitForSeconds(_attackCoolDown);
        _waitDie = new WaitForSeconds(_timeToDie);
        _animator = GetComponent<MobAnimator>();
        _coinSpawner = GetComponent<CoinSpawner>();
        _health = GetComponent<Health>();
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
        _animator.SetAtacked();
        
        if(_health.IsAlive == false)
            Die();
    }
    
    private void Die()
    {
        _coinSpawner.SpawnBurst();
        StartCoroutine(WaitDie());
    }

    private IEnumerator AttackCooldown()
    {
        _isAbleToAttack = false;
        yield return _waitAttack;
        _isAbleToAttack = true;
    }
    
    private IEnumerator WaitDie()
    {
        yield return _waitDie;
        Dead?.Invoke();
        _spawner.MobDead(_spawnId);
    }
    
    public void Attack(Player player)
    {
        if (_isAbleToAttack)
        {
            player.TakeDamage(_baseDamage);
            StartCoroutine(AttackCooldown());
        }
    }

    public void SetAlive()
    {
        _health.Heal(_health.MaxValue);
    }

    public void SetSpawner(MobSpawner spawner, int id)
    {
        _spawnId = id;
        _spawner = spawner;
    }
    
    public void SetRoute(Transform route)
    {
        _wanderer.SetWaypointsParent(route);  
    }
}
