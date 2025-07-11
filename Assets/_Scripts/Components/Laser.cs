using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject beam;
    [SerializeField] float _laserLength;
    void OnEnable()
    {
        PlayerInput.Input.OnAct3Pressed += ActivateLaser;
        PlayerInput.Input.OnAct3Canceled += DeactivateLaser;
    }
    void OnDisable()
    {
        PlayerInput.Input.OnAct3Pressed -= ActivateLaser;
        PlayerInput.Input.OnAct3Canceled -= DeactivateLaser;
    }
    void ActivateLaser()
    {
        beam.transform.localScale = new Vector2(_laserLength, beam.transform.localScale.y);
    }
    void DeactivateLaser()
    {
        beam.transform.localScale = new Vector2(beam.transform.localScale.y, beam.transform.localScale.y);
    }
}
