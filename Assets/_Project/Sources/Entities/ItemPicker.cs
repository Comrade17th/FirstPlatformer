using System;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public event Action<string> PickedItem;
    public event Action<int> PickedCoin;
    public event Action<int> PickedMedicine;
    
    public void Pick(PickableItem item)
    {
    }
    
    private void Pick(PickableCoin coin)
    {
        PickedCoin?.Invoke(coin.Amount);
    }
    
    private void Pick(PickableMedicine medicine)
    {
        PickedMedicine?.Invoke(medicine.HealthRegen);
    }
}
