using UnityEngine;
using System.Collections.Generic;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] ShipComponent component;
    [SerializeField] LayerMask _ignoredLayers;
    Health collisionHealth;
    const int UNITS_PER_LASERSCALE = 8;
    [SerializeField] float _laserLength;
    [SerializeField] float _laserWidth;
    [SerializeField] float _laserLengthExtraBit;
    float _currentLaserLength;
    void Awake()
    {
        _currentLaserLength = _laserLength;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _ignoredLayers) != 0)
        {
            return;
        }
        if (collisionHealth == null)
        {
            collisionHealth = collision.GetComponent<Health>();
        }
        Bounds myBounds = GetComponent<Collider2D>().bounds;
        Bounds otherBounds = collision.bounds;
        Vector3 contactApprox = myBounds.ClosestPoint(otherBounds.center);

        _currentLaserLength = (transform.position - contactApprox).magnitude + _laserLengthExtraBit;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _ignoredLayers) != 0)
        {
            return;
        }
        collisionHealth = null;
        _currentLaserLength = _laserLength;
    }
    public float GetLaserLength()
    {
        return _currentLaserLength * UNITS_PER_LASERSCALE;
    }
    public float GetLaserWidth()
    {
        return _laserWidth;
    }
    void Update()
    {
        if (collisionHealth != null)
        {
            collisionHealth.TakeDamage(component.Stats.Damage * Time.deltaTime);
        }
    }
}
