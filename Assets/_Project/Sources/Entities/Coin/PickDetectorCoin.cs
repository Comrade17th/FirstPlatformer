using UnityEngine;

public class PickDetectorCoin : PickDetector
{
    [SerializeField] private PickableCoin _coin;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ItemPicker picker))
        {
            picker.Pick(_coin);
            gameObject.SetActive(false);
        }
    }
}
