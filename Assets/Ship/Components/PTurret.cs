using UnityEngine;
using UnityEngine.InputSystem;

public class PTurret : MonoBehaviour
{
    [SerializeField] GameObject projectile;
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
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
