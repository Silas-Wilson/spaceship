using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float _accelerationStrength;
    float _rotationStrength;
    float _maxSpeed;
    float _maxRotationSpeed;
    const float INERTIAL_FACTOR = 10f;
    Rigidbody2D _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void UpdateMovementStats(float acc, float rotAcc, float max, float rotMax)
    {
        _accelerationStrength = acc;
        _rotationStrength = rotAcc;
        _maxSpeed = max;
        _maxRotationSpeed = rotMax;
    }
    void FixedUpdate()
    {
        #region Acceleration

        float targetSpeed = PlayerInput.Input.IsAccelerating ? _maxSpeed : 0f;

        //Forward Motion
        float currentForwardSpeed = Vector2.Dot(transform.right, _rb.linearVelocity);
        _rb.AddForce(transform.right * _accelerationStrength * (targetSpeed - currentForwardSpeed));

        //Perpindicular Motion
        float currentPerpindicularSpeed = Vector2.Dot(transform.up, _rb.linearVelocity);
        _rb.AddForce(-1 * transform.up * INERTIAL_FACTOR * currentPerpindicularSpeed);

        #endregion

        #region Turning
        int steerDir = PlayerInput.Input.SteerDirection;
        float targetRotationSpeed = steerDir != 0 ? _maxRotationSpeed : 0f;
        _rb.AddTorque(_rotationStrength * (-1 * steerDir * targetRotationSpeed - _rb.angularVelocity));

        #endregion
    }
}
