using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _size;
    [SerializeField] Rigidbody2D _rb;
    void Awake()
    {
        transform.localScale = Vector3.one * _size;
        _rb.mass = _size * _size;
    }
    public void DestroyAsteroid()
    {
        Destroy(gameObject);
    }
}
