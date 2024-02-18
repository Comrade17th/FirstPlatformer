using System;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public event Action<string> PickedItem;
    public event Action<int> PickedCoin;
    public event Action<float> PickedMedicine;
    
    public void Pick(PickableItem item)
    {
    }
    
    public void Pick(PickableCoin coin)
    {
        PickedCoin?.Invoke(coin.Amount);
    }
    
    public void Pick(PickableMedicine medicine)
    {
        PickedMedicine?.Invoke(medicine.HealthRegen);
    }
}
