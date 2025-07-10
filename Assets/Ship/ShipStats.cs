using System.Runtime.InteropServices;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [field: SerializeField] public float _baseHP { get; private set; }
    [field: SerializeField] public float _baseAcceleration { get; private set; }
    [field: SerializeField] public float _baseRotationalAcceleration { get; private set; }
    [field: SerializeField] public float _baseMaxSpeed { get; private set; }
    [field: SerializeField] public float _baseMaxRotationalSpeed { get; private set; }
    public float _mass { get; private set; }
    public float _hp { get; private set; }
    public float _acceleration { get; private set; }
    public float _rotationalAcceleration { get; private set; }
    public float _maxSpeed { get; private set; }
    public float _maxRotationSpeed { get; private set; }
    //Implement CoM later
    Vector2 _centerOfMass;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Health health;
    [SerializeField] PlayerMovement movement;
    void Start()
    {
        UpdateShipStats();
    }
    public void UpdateShipStats()
    {
        ShipComponent[] allComponents = GetComponentsInChildren<ShipComponent>();

        _mass = FindTotalMass();
        rb.mass = _mass;

        _hp = _baseHP;
        _acceleration = _baseAcceleration;
        _rotationalAcceleration = _baseRotationalAcceleration;
        _maxSpeed = _baseMaxSpeed;
        _maxRotationSpeed = _baseMaxRotationalSpeed;
        foreach (ShipComponent component in allComponents)
        {
            Debug.Log(component);
            _hp += component.bonusHP;
            _acceleration += component.bonusAcc;
            _rotationalAcceleration += component.bonusRotAcc;
            _maxSpeed += component.bonusMax;
            _maxRotationSpeed += component.bonusRotMax;
        }

        health.SetMaxHealth(_hp);

        movement.UpdateMovementStats();
    }
    float FindTotalMass()
    {
        float mass = 0;
        ShipComponent[] components = ShipBuildData.Instance.Grid.GetAllComponents();

        foreach (ShipComponent component in components)
        {
            mass += component.mass;
        }

        return mass;
    }
}
