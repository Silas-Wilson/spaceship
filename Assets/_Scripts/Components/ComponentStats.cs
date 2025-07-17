using UnityEngine;

[CreateAssetMenu(fileName = "ComponentStats", menuName = "Component Stat")]
public class ComponentStats : ScriptableObject
{
    [field: SerializeField] public float Mass { get; private set; }
    [field: SerializeField] public float Defense { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float ProjectileSpeed { get; private set; }
    [field: SerializeField] public float ProjectileDuration { get; private set; }
    [field: SerializeField] public float ThrustStrength { get; private set; }
    [field: SerializeField] public float BonusHP { get; private set; }
    [field: SerializeField] public float BonusAcceleration { get; private set; }
    [field: SerializeField] public float BonusRotationalAcceleration { get; private set; }
    [field: SerializeField] public float BonusMaxSpeed { get; private set; }
    [field: SerializeField] public float BonusMaxRotationalSpeed { get; private set; }
}
