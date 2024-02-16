using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxValue { get; private set; }
    
    public bool IsAlive => _currentValue > 0;
    public event Action<float> ValueChanged;

    private float _currentValue;

    private void Awake()
    {
        _currentValue = MaxValue;
    }

    private void Start()
    {
        ValueChanged?.Invoke(_currentValue);
    }

    public void TakeDamage(float value)
    {
        _currentValue -= value;
        _currentValue = Mathf.Clamp(_currentValue, 0, MaxValue);
        
        ValueChanged?.Invoke(_currentValue);
    }

    public void Heal(float value)
    {
        _currentValue += value;
        _currentValue = Mathf.Clamp(_currentValue, 0, MaxValue);
        
        ValueChanged?.Invoke(_currentValue);
    }
}
