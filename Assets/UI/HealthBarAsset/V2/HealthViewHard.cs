using UnityEngine;
using UnityEngine.UI;

public class HealthViewHard : HealthView
{
    [SerializeField] private Slider _healthSlider;

    public override void UpdateHealth(float targetValue)
    {
        _healthSlider.value = targetValue / MaxHealth;
    }
}
