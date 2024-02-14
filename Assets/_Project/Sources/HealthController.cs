using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _health = 100f;

    public float MaxHealth => _maxHealth;
    public float Health => _health;

    public event Action<float> HealthChanged;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(-damage);
        
        if (_health < 0)
            _health = 0;
    }

    public void Heal(float healthRegeneration)
    {
        _health += healthRegeneration;
        HealthChanged?.Invoke(healthRegeneration);
        
        if (_health >= _maxHealth)
            _health = MaxHealth;
    }

    public void SetAlive()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke(_maxHealth);
    }
}
