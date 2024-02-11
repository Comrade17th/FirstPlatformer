using System;
using UnityEditor;
using UnityEngine;

public class PickDetectorMedicine : PickDetector
{
    [SerializeField] private PickableMedicine _medicine;

    private ItemPicker _picker;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ItemPicker picker))
        {
            Debug.Log("Picker found");   
            _picker = picker;
        }

        if (other.TryGetComponent(out PlayerMovement player))
        {
            Debug.Log("Player found");
            if (_picker)
            {
                Debug.Log("Medicine picked");
                _picker.Pick(_medicine);
                gameObject.SetActive(false);    
            }
        }
    }
}
