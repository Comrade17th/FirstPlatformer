using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animation _animation;
    
    public void Launch(Vector2 force)
    {
        _rb.AddForce(force, ForceMode2D.Impulse);
    }
}
