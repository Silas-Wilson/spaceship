using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject beam;
    [SerializeField] float _laserLength;
    [SerializeField] float _laserWidth;
    const int UNITS_PER_LASERSCALE = 8;
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
        beam.transform.localScale = new Vector2(UNITS_PER_LASERSCALE * _laserLength, _laserWidth);
    }
    void DeactivateLaser()
    {
        beam.transform.localScale = Vector2.zero;
    }
}
