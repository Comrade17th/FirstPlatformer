using UnityEngine;
using UnityEngine.UI;

public class HealthViewHard : HealthView
{
    [SerializeField] private Slider _healthSlider;

    private void Awake()
    {
        _healthSlider.maxValue = MaxHealth;
    }

    public override void UpdateHealth(float targetValue)
    {
        _healthSlider.value = targetValue;
    }
}
