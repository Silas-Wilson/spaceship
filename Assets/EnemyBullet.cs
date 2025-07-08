using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float _duration;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }
}
