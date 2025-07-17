using System.Collections;
using System.Data.Common;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] LayerMask _ignoredLayers;
    [SerializeField] float _duration;
    [SerializeField] float _damage;
    [SerializeField] Rigidbody2D rb;
    void Start()
    {
        StartCoroutine(LifeTimer());
    }
    public void SetVelocity(float speed, Vector2 direction)
    {
        rb.linearVelocity = speed * direction;
    }

    IEnumerator LifeTimer()
    {
        float timeLeft = _duration;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        OnDestroy();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _ignoredLayers) != 0)
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponentInParent<Health>();
            float playerDefense = collision.GetComponent<ShipComponent>().Defense;

            playerHealth.TakeDamage(_damage * (1 - (playerDefense / 100)));
        }
        else if (collision.TryGetComponent(out Health colliderHealth))
        {
            colliderHealth.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }
}
