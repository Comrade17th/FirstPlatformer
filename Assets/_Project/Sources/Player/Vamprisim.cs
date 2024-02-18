using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Vamprisim : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _timeStealStep = 0.5f;
    [SerializeField] private float _healthStealStep = 4f;

    [SerializeField] private List<Transform> _enemiesInZone;
    private Transform _targetEnemy;
    private WaitForSeconds _waitDrink;
    private Coroutine _coroutine;

    private void Awake()
    {
        _enemiesInZone = new List<Transform>();
        _waitDrink = new WaitForSeconds(_timeStealStep);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Mob enemy))
        {
            _enemiesInZone.Add(enemy.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Mob enemy))
        {
            _enemiesInZone.Remove(enemy.transform);
        }
    }
    private IEnumerator BloodDrinking(Mob enemy)
    {
        for (float i = 0; i < _duration; i += _timeStealStep)
        {
            if (enemy.IsAlive)
            {
                enemy.TakeDamage(_healthStealStep);
                _health.Heal(_healthStealStep);
                yield return _waitDrink;
            }
            else
            {
                yield break;
            }   
        }
    }

    private bool TryGetClosestEnemy(out Mob enemy)
    {
        bool isExist = false;
        enemy = null;
        Transform enemyTransform = Utilities.GetClosestTransform(transform, _enemiesInZone);

        if (enemyTransform != null)
        {
            enemy = enemyTransform.GetComponent<Mob>();
            isExist = true;
        }

        return isExist;
    }
    
    public void DrinkBlood()
    {
        InterruptDrinking();
        
        if(TryGetClosestEnemy(out Mob enemy))
            _coroutine = StartCoroutine(BloodDrinking(enemy));
    }

    public void InterruptDrinking()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = null;
    }
}
