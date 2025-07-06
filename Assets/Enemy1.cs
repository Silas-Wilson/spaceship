using System.Collections;
using UnityEngine;
using State = EnemyStates.State;
public class Enemy1 : MonoBehaviour
{
    const float PI = Mathf.PI;
    [Header("Movement")]
    [SerializeField] float _maxSpeed;
    [SerializeField] float _maxFleeSpeed;
    [SerializeField] float _maxRotationalSpeed;
    [SerializeField] float _thrustForce;
    [Header("Specific Movement")]
    [SerializeField] float _preferredDistanceFromPlayer;
    [SerializeField] float _unacceptableDistanceFromPlayer;
    [Header("Attack")]
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _bulletCooldownTime;
    [SerializeField] float _burstCooldownTime;
    [SerializeField] int _burstNumber;
    bool canFire;

    [Header("Components")]
    [SerializeField] EnemyDetection detection;
    [SerializeField] EnemyStates sm;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] EnemyBullet bullet;

    void Start()
    {
        canFire = true;
    }
    void FixedUpdate()
    {
        State currentState = sm.GetState();
        GameObject player = detection.TryGetPlayer();
        if (detection.IsPlayerVisible)
        {
            sm.SetState(State.active);
        }
        else
        {
            sm.SetState(State.idle);
        }
        if (currentState == State.idle)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if (currentState == State.active)
        {
            #region Movement

            float playerDistance = (player.transform.position - transform.position).magnitude;
            Vector2 playerDirection = (player.transform.position - transform.position).normalized;

            float targetSpeed = _maxSpeed;
            Vector2 accelerationDirection = playerDirection;

            if (playerDistance > _preferredDistanceFromPlayer)
            {
                //Do nothing
            }
            else if (playerDistance < _preferredDistanceFromPlayer && playerDistance > _unacceptableDistanceFromPlayer)
            {
                accelerationDirection = new Vector2(-1 * playerDirection.y, playerDirection.x);
            }
            else
            {
                accelerationDirection *= -1;
                targetSpeed = _maxFleeSpeed;
            }
            rb.AddForce(accelerationDirection * _thrustForce * (targetSpeed - rb.linearVelocity.magnitude));

            #endregion

            #region Attacks

            Vector2 playerVelocity = player.GetComponentInParent<Rigidbody2D>().linearVelocity;

            Vector2 playerParallelVelocity = Vector2.Dot(playerVelocity, playerDirection) * playerDirection;
            Vector2 playerPerpendicularVelocity = playerVelocity - playerParallelVelocity;
            float playerPerpendicularSpeed = playerPerpendicularVelocity.magnitude;

            float cross = playerDirection.x * playerPerpendicularVelocity.y - playerDirection.y * playerPerpendicularVelocity.x;

            float ratio = playerPerpendicularSpeed / _bulletSpeed;
            ratio = Mathf.Clamp(ratio, -1f, 1f);

            float fireAngle = Mathf.Asin(ratio);

            if (canFire)
            {
                rb.linearVelocity = Vector2.zero;
                EnemyBullet currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                currentBullet.SetVelocity(_bulletSpeed, Rotate(playerDirection, Mathf.Sign(cross) * fireAngle));//Rotate(playerDirection, fireAngle));
                StartCoroutine(BulletCooldown());
            }

            #endregion
        }
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    IEnumerator BulletCooldown()
    {
        float timeLeft = _bulletCooldownTime;
        canFire = false;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        canFire = true;
    }
    IEnumerator BurstCooldown()
    {
        float timeLeft = _bulletCooldownTime;
        canFire = false;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        canFire = true;
    }

    Vector2 Rotate(Vector2 v, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
}
