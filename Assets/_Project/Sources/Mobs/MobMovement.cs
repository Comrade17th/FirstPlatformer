using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MobMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private bool _isFacingRight = true;

    public float Speed => _speed;

    public void Follow(Vector3 target)
    {
        Flip(target);
        
        transform.position = Vector3.MoveTowards(
            transform.position,
            target, 
            _speed * Time.deltaTime);
    }
    
    private void Flip(Vector3 target)
    {
        float direction = target.x - transform.position.x;
        
        if (_isFacingRight && direction < 0f || !_isFacingRight && direction > 0f)
        {
            Vector3 localScale = transform.localScale;

            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
