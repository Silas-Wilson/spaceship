using System.Collections;
using UnityEngine;
public class PTurret : MonoBehaviour
{
    [SerializeField] float _cooldownTime;
    [SerializeField] GameObject projectile;
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
        GameObject thisProjectile = Instantiate(projectile, spawnPos, transform.rotation);
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
        float timeLeft = _cooldownTime;

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
