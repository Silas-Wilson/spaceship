using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float _projectileSpeed;
    [SerializeField] float _DurationSecs;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.linearVelocity = transform.right * _projectileSpeed;
        StartCoroutine(LifetimeTimer());
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    IEnumerator LifetimeTimer()
    {
        float timeLeft = _DurationSecs;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
