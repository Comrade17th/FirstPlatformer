using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackController : MonoBehaviour
{
    [SerializeField] private List<Mob> _mobsToAttack;

    private void Start()
    {
        _mobsToAttack = new List<Mob>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Mob mob))
        {
            _mobsToAttack.Add(mob);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Mob mob))
        {
            _mobsToAttack.Remove(mob);
        }
    }

    public void Attack(float _damage)
    {
        foreach (Mob mob in _mobsToAttack)
        {
            mob.TakeDamage(_damage);
        }
    }
}
