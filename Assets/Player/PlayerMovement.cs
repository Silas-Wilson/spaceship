using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _rotationStrength;
    [SerializeField] float _accelerationStrength;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (PlayerInput.Input.SteerDirection != 0)
        {
            _rb.AddTorque(-1 * _rotationStrength * PlayerInput.Input.SteerDirection);
        }
        if (PlayerInput.Input.IsAccelerating)
        {
            _rb.AddForce(transform.right * _accelerationStrength);
        }
    }
}
