using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> ValueChanged;

    private float _currentValue;
    
    [field: SerializeField] public float MaxValue { get; private set; }

    public bool IsAlive => _currentValue > 0;

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
        float modifier = -1;
        
        ChangeValue(value, modifier, _currentValue);
    }

    public void Heal(float value)
    {
        float maxValue = MaxValue - _currentValue;
        float modifier = 1;
        
        ChangeValue(value, modifier, maxValue);
    }
    
    private void ChangeValue(float value, float modifier, float maxValue)
    {
        value = Mathf.Clamp(value, 0, maxValue);
        _currentValue += modifier * value;
        
        ValueChanged?.Invoke(_currentValue);
    }
}
