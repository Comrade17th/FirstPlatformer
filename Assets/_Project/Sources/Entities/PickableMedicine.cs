using UnityEngine;

public class PickableMedicine : PickableItem
{
    [SerializeField] private int _healthRegen = 10;

    public int HealthRegen => _healthRegen;
}
