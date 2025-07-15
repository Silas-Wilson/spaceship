using UnityEngine;

[CreateAssetMenu(fileName = "ComponentStats", menuName = "Component Stat")]
public class ComponentStats : ScriptableObject
{
    //"_d" signifies that these are the default stats
    [Header("Universal Stats")]
    [SerializeField] float _dMass;
    [SerializeField] float _dDefense;
    [Header("Specific Stats")]
    [SerializeField] float _dDamage;
    [SerializeField] float _dFireRate;
    [SerializeField] float _dProjectileSpeed;
    [SerializeField] float _dProjectileDuration;
    [SerializeField] float _dThrustStrength;
    [Header("Ship Bonuses Stats")]
    [SerializeField] float _dBonusHP;
    [SerializeField] float _dBonusAcceleration;
    [SerializeField] float _dBonusRotationalAcceleration;
    [SerializeField] float _dBonusMaxSpeed;
    [SerializeField] float _dBonusMaxRotationalSpeed;

    [HideInInspector] public float Mass;
    [HideInInspector] public float Defense;
    [HideInInspector] public float Damage;
    [HideInInspector] public float FireRate;
    [HideInInspector] public float ProjectileSpeed;
    [HideInInspector] public float ProjectileDuration;
    [HideInInspector] public float ThrustStrength;
    [HideInInspector] public float BonusHP;
    [HideInInspector] public float BonusAcceleration;
    [HideInInspector] public float BonusRotationalAcceleration;
    [HideInInspector] public float BonusMaxSpeed;
    [HideInInspector] public float BonusMaxRotationalSpeed;

    void Awake()
    {
        Mass = _dMass;
        Defense = _dDefense;
        Damage = _dDamage;
        FireRate = _dFireRate;
        ProjectileSpeed = _dProjectileSpeed;
        ProjectileDuration = _dProjectileDuration;
        ThrustStrength = _dThrustStrength;
        BonusHP = _dBonusHP;
        BonusAcceleration = _dBonusAcceleration;
        BonusRotationalAcceleration = _dBonusRotationalAcceleration;
        BonusMaxSpeed = _dBonusMaxSpeed;
        BonusMaxRotationalSpeed = _dBonusMaxRotationalSpeed;
    }
}
