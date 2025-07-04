using UnityEngine;
public class PTurret : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Vector3 _projectileSpawnPos;
    void OnEnable()
    {
        PlayerInput.Input.OnAct1Pressed += FireProjectile;
    }
    void OnDisable()
    {
        PlayerInput.Input.OnAct1Pressed -= FireProjectile;
    }
    void FireProjectile()
    {
        Vector3 spawnPos = transform.position + transform.TransformDirection(_projectileSpawnPos);
        Instantiate(projectile, spawnPos, transform.rotation);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + _projectileSpawnPos, Vector3.one * 0.05f);
    }
}
