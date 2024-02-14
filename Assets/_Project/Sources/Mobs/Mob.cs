using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(MobAnimator))]
[RequireComponent(typeof(Wanderer))]
public class Mob : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private Wanderer _wanderer;
    [SerializeField] private HealthController _healthController;
    // [SerializeField] private float _healthBase = 35;
    // [SerializeField] private float _health;
    [SerializeField] private float _baseDamage = 10;
    [SerializeField] private float _attackCoolDown = 2f;
    [SerializeField] private float _timeToDie = 1f;
    
    private MobAnimator _animator;
    private MobSpawner _spawner;
    
    private int _spawnId;
    private WaitForSeconds _waitDie;
    private WaitForSeconds _waitAttack;
    private bool _isAbleToAttack = true;

    public event Action Dead;

    private void Awake()
    {
        _waitAttack = new WaitForSeconds(_attackCoolDown);
        _waitDie = new WaitForSeconds(_timeToDie);
        _animator = GetComponent<MobAnimator>();
        _coinSpawner = GetComponent<CoinSpawner>();
    }

    public void TakeDamage(float damage)
    {
        _healthController.TakeDamage(damage);
        _animator.SetAtacked();
        
        if(_healthController.Health <= 0)
            Die();
    }
    
    private void Die()
    {
        Debug.Log("Starting burst");
        _coinSpawner.SpawnBurst();
        Debug.Log("Ending burst");
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
        _healthController.SetAlive();
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
