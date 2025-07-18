using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    [SerializeField] ComponentStats stats;
    [HideInInspector] public float Mass;
    [HideInInspector] public float Defense;
    [HideInInspector] public float Damage;
    public float FireRate;
    [HideInInspector] public float ProjectileSpeed;
    [HideInInspector] public float ProjectileDuration;
    [HideInInspector] public float ThrustStrength;
    [HideInInspector] public float BonusHP;
    [HideInInspector] public float BonusAcceleration;
    [HideInInspector] public float BonusRotationalAcceleration;
    [HideInInspector] public float BonusMaxSpeed;
    [HideInInspector] public float BonusMaxRotationalSpeed;

    public void Init(Vector3Int spawnLocation, Quaternion spawnRotation)
    {
        transform.localPosition = spawnLocation;
        transform.rotation = spawnRotation;

        Mass = stats.Mass;
        Defense = stats.Defense;
        Damage = stats.Damage;
        FireRate = stats.FireRate;
        ProjectileSpeed = stats.ProjectileSpeed;
        ProjectileDuration = stats.ProjectileDuration;
        ThrustStrength = stats.ThrustStrength;
        BonusHP = stats.BonusHP;
        BonusAcceleration = stats.BonusAcceleration;
        BonusRotationalAcceleration = stats.BonusRotationalAcceleration;
        BonusMaxSpeed = stats.BonusMaxSpeed;
        BonusMaxRotationalSpeed = stats.BonusMaxRotationalSpeed;

        //Debug.Log($"{gameObject.name} has been initialized at {transform.position}");
    }
}
