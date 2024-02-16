using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected float MaxHealth => _health.MaxValue;

    private void OnEnable()
    {
        _health.ValueChanged += UpdateHealth;
    }
    
    private void OnDisable()
    {
        _health.ValueChanged -= UpdateHealth;
    }

    public abstract void UpdateHealth(float targetValue);
}
