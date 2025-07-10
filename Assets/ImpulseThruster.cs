using System.Collections;
using UnityEngine;

public class ImpulseThruster : MonoBehaviour
{
    [SerializeField] float _thrustStrength;
    [SerializeField] float _forceDuration;
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
        float timeLeft = _forceDuration;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            shipRb.AddForce(_thrustStrength * transform.right);
            yield return null;
        }
    }
    
}
