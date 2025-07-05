using UnityEngine;
using State = EnemyStates.State;
public class Enemy1 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _maxSpeed;
    [SerializeField] float _maxFleeSpeed;
    [SerializeField] float _maxRotationalSpeed;
    [SerializeField] float _thrustForce;
    [Header("Specific Movement")]
    [SerializeField] float _preferredDistanceFromPlayer;
    [SerializeField] float _unacceptableDistanceFromPlayer;

    [Header("Components")]
    [SerializeField] EnemyDetection detection;
    [SerializeField] EnemyStates sm;
    [SerializeField] Rigidbody2D rb;
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
        if (currentState == State.active)
        {
            float playerDistance = (player.transform.position - transform.position).magnitude;
            Vector2 playerDirection = (player.transform.position - transform.position).normalized;
            Vector2 perpindicularDirection = new Vector2(-1 * playerDirection.y, playerDirection.x);
            float parallelVelocity = Vector2.Dot(playerDirection, rb.linearVelocity);

            if (playerDistance > _preferredDistanceFromPlayer)
            {
                rb.AddForce(playerDirection * _thrustForce * (_maxSpeed - rb.linearVelocity.magnitude));
            }
            else if (playerDistance < _preferredDistanceFromPlayer && playerDistance > _unacceptableDistanceFromPlayer)
            {
                rb.AddForce(perpindicularDirection * _thrustForce * (_maxSpeed - rb.linearVelocity.magnitude));
            }
            else
            {
                rb.AddForce(-1 * playerDirection * _thrustForce * (_maxFleeSpeed - rb.linearVelocity.magnitude));
            }
        }
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
