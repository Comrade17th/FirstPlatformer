using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PickableCoin))]
public class PickDetectorCoin : PickDetector
{
    private PickableCoin _coin;

    private void Awake()
    {
        _coin = GetComponent<PickableCoin>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ItemPicker picker))
        {
            picker.Pick(_coin);
            Destroy(_coin.gameObject);
        }
    }
}
