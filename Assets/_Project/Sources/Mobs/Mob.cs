using System;
using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(MobAnimator))]
[RequireComponent(typeof(Wanderer))]
public class Mob : MonoBehaviour
{
    [SerializeField] private float _health = 35;
    [SerializeField] private float _baseDamage = 10;
    [SerializeField] private Wanderer _wanderer;
    
    private MobAnimator _animator;
    private MobSpawner _spawner;
    private int _spawnId;

    private void Start()
    {
        _animator = GetComponent<MobAnimator>();
    }

    private void Attack(Player player)
    {
        player.TakeDamage(_baseDamage);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _animator.SetAtacked();
        
        if(_health <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Mob is dead");
        _spawner.MobDead(_spawnId);
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
