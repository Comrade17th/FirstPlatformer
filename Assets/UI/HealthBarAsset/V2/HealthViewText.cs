using TMPro;
using UnityEngine;

public class HealthViewText : HealthView
{
    [SerializeField] private TextMeshProUGUI _tmpText;
    
    public override void UpdateHealth(float targetValue)
    {
        _tmpText.text = $"{targetValue}/{MaxHealth}";
    }
}
