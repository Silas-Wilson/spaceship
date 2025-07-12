using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LaserBeam beam;
    bool laserActivated;
    void Start()
    {
        laserActivated = false;
    }
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
        laserActivated = true;
    }
    void DeactivateLaser()
    {
        laserActivated = false;
    }
    void Update()
    {
        if (laserActivated)
        {
            beam.transform.localScale = new Vector2(beam.GetLaserLength(), beam.GetLaserWidth());
        }
        else
        {
            beam.transform.localScale = Vector2.zero;
        }
    }
}
