using UnityEngine;

public class PickDetector : MonoBehaviour
{
    [SerializeField] private PickableItem _pickableItem;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ItemPicker picker))
        {
            picker.Pick(_pickableItem);
            gameObject.SetActive(false);
        }
    }
}
