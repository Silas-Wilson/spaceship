using System.Collections;
using UnityEngine;
public class PTurret : MonoBehaviour
{
    [SerializeField] ShipComponent _component;
    [SerializeField] PlayerProjectile projectile;
    [SerializeField] Vector3 _projectileSpawnPos;
    bool canFire;
    bool isFiring;
    void Start()
    {
        isFiring = false;
        canFire = true;
    }
    void OnEnable()
    {
        PlayerInput.Input.OnAct1Pressed += StartFiring;
        PlayerInput.Input.OnAct1Canceled += EndFiring;
    }
    void OnDisable()
    {
        PlayerInput.Input.OnAct1Pressed -= StartFiring;
        PlayerInput.Input.OnAct1Canceled -= EndFiring;
    }
    void Update()
    {
        if (canFire & isFiring)
        {
            FireProjectile();
        }
    }
    void FireProjectile()
    {
        Vector3 spawnPos = transform.position + transform.TransformDirection(_projectileSpawnPos);
        PlayerProjectile thisProjectile = Instantiate(projectile, spawnPos, transform.rotation);
        thisProjectile.Initialize(_component);
        StartCoroutine(cooldown());
    }
    void StartFiring()
    {
        isFiring = true;
    }
    void EndFiring()
    {
        isFiring = false;
    }

    IEnumerator cooldown()
    {
        canFire = false;
        float timeLeft = 1 / _component.Stats.FireRate;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        canFire = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + _projectileSpawnPos, Vector3.one * 0.05f);
    }
}
