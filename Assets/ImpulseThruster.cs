using UnityEngine;

public class ImpulseThruster : MonoBehaviour
{
    [SerializeField] float _thrustStrength;
    Rigidbody2D shipRb;
    void Awake()
    {
        shipRb = GetComponentInParent<Rigidbody2D>();
    }
    void OnEnable()
    {
        PlayerInput.Input.OnAct2Pressed += ActivateThrust;
    }
    void OnDisable()
    {
        PlayerInput.Input.OnAct2Pressed -= ActivateThrust;
    }

    void ActivateThrust()
    {
        shipRb.AddForce(_thrustStrength * transform.right, ForceMode2D.Impulse);
    }
}
