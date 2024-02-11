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
        Debug.Log("coin");
        PickedCoin?.Invoke(coin.Amount);
        Debug.Log("Invoked");
    }
    
    public void Pick(PickableMedicine medicine)
    {
        Debug.Log("Medicine Picked");
        PickedMedicine?.Invoke(medicine.HealthRegen);
    }
}
