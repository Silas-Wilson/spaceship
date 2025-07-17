using System.Collections;
using UnityEngine;

public class ImpulseThruster : MonoBehaviour
{
    [SerializeField] ShipComponent _component;
    const float FORCE_DURATION = 0.2f;
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
        StartCoroutine(ForceApplier());
    }

    IEnumerator ForceApplier()
    {
        float timeLeft = FORCE_DURATION;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            shipRb.AddForce(_component.ThrustStrength * transform.right);
            yield return null;
        }
    }
    
}
