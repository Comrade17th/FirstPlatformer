using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableCoin : PickableItem
{
    [SerializeField, Min(0)] private int _amount = 1;
    
    public int Amount => _amount;
}
