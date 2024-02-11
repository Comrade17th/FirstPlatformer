using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Follower : MonoBehaviour
{
    [SerializeField] private Mob _mob;
    [SerializeField] private MobMovement _mobMovement;
    [SerializeField] private MobBehavior _behavior;
    [SerializeField] private float _timeFollowMiss = 3;

    private WaitForSeconds _waitMiss;
    private Transform _target;
    [SerializeField] private bool _isFollow = false;

    private Coroutine _waitAfterMiss;
    private void Start()
    {
        _waitMiss = new WaitForSeconds(_timeFollowMiss);
    }

    private void Update()
    {
        if (_isFollow)
        {
            _mobMovement.Follow(_target.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StartFollowing();
            _target = player.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if(_mob.enabled)
                _waitAfterMiss = StartCoroutine(TimeMiss());
        }
    }

    private void OnEnable()
    {
        _mob.Dead += StopFollowing;
    }

    private void OnDisable()
    {
        _mob.Dead -= StopFollowing;
    }

    private void StartFollowing()
    {
        _isFollow = true;
        _behavior.StartFollowing();
    }
    
    private void StopFollowing()
    {
        _isFollow = false;
        _behavior.StopFollowing();
    }

    private IEnumerator TimeMiss()
    {
        yield return _waitMiss;
        StopFollowing();
    }
}
