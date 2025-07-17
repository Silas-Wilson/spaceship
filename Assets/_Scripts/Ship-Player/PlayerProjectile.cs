using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] LayerMask _ignoredLayers;
    ShipComponent source;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Initialize(ShipComponent s)
    {
        source = s;
        rb.linearVelocity = transform.right * source.ProjectileSpeed;
        StartCoroutine(LifetimeTimer());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _ignoredLayers) != 0)
        {
            return;
        }
        if (collision.TryGetComponent(out Health colliderHealth))
        {
            Debug.Log($"{collision.name} has taken {source.Damage} damage!");
            colliderHealth.TakeDamage(source.Damage);
        }
        Destroy(gameObject);
    }
    IEnumerator LifetimeTimer()
    {
        float timeLeft = source.ProjectileDuration;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
