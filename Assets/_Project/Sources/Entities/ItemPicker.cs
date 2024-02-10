using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public event Action<string> PickedItem;
    public event Action<int> PickedCoin;
    public event Action<int> PickedMedicine;
    
    public void Pick(PickableItem item)
    {
        if(item is PickableCoin)
            Pick(item as PickableCoin);
        
        if(item is PickableMedicine)
            Pick(item as PickableMedicine);
    }
    
    private void Pick(PickableCoin coin)
    {
        PickedCoin?.Invoke(coin.Amount);
        Debug.Log("Invoked");
    }
    
    private void Pick(PickableMedicine medicine)
    {
        Debug.Log("Picked Medicine");
        PickedMedicine?.Invoke(medicine.HealthRegen);
    }
}
