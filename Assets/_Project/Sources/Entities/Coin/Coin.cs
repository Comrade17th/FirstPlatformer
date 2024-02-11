using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animation _animation;
    
    public void Launch(Vector2 force)
    {
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
