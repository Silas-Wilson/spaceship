using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float _projectileSpeed;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.linearVelocity = transform.right * _projectileSpeed;
    }

}
