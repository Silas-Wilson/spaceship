using System.Runtime.InteropServices;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] float _baseHP;
    [SerializeField] float _baseAcceleration;
    [SerializeField] float _baseRotationalAcceleration;
    [SerializeField] float _baseMaxSpeed;
    [SerializeField] float _baseMaxRotationalSpeed;
    float _mass;
    float _hp;
    float _acceleration;
    float _rotationalAcceleration;
    float _maxSpeed;
    float _maxRotationalSpeed;
    //Implement CoM later
    Vector2 _centerOfMass;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Health health;
    [SerializeField] PlayerMovement movement;
    public void UpdateShipStats()
    {
        ShipComponent[] components = ShipBuildData.Instance.Grid.GetAllComponents();

        _mass = 0f;

        _hp = _baseHP;
        _acceleration = _baseAcceleration;
        _rotationalAcceleration = _baseRotationalAcceleration;
        _maxSpeed = _baseMaxSpeed;
        _maxRotationalSpeed = _baseMaxRotationalSpeed;

        foreach (ShipComponent component in components)
        {
            BuffComponent buff = component.gameObject.GetComponent<BuffComponent>();
            if (buff == null)
            {
                continue;
            }
            buff.ApplyBuffs();
        }

        foreach (ShipComponent component in components)
        {
            _mass += component.Mass;

            _hp += component.BonusHP;
            _acceleration += component.BonusAcceleration;
            _rotationalAcceleration += component.BonusRotationalAcceleration;
            _maxSpeed += component.BonusMaxSpeed;
            _maxRotationalSpeed += component.BonusMaxRotationalSpeed;
        }
        rb.mass = _mass;
        health.SetMaxHealth(_hp);
        movement.UpdateMovementStats(_acceleration, _rotationalAcceleration, _maxSpeed, _maxRotationalSpeed);
    }
}
