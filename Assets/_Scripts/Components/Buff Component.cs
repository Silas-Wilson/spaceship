using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BuffComponent : MonoBehaviour
{
    [SerializeField] ShipComponent _component;
    [SerializeField] ComponentStats _buffedStats;
    [SerializeField] bool _buffUp;
    [SerializeField] bool _buffDown;
    [SerializeField] bool _buffRight;
    [SerializeField] bool _buffLeft;
    const float overlapCircleSize = 0.1f;
    public void ApplyBuffs()
    {
        List<ShipComponent> componentsToBuff = GetBuffedComponents();

        foreach (ShipComponent component in componentsToBuff)
        {
            Debug.Log($"Buffing {component.GetInstanceID()}");
            component.Mass += _buffedStats.Mass;
            component.Defense += _buffedStats.Defense;
            component.Damage += _buffedStats.Damage;
            component.FireRate += _buffedStats.FireRate;
            component.ProjectileSpeed += _buffedStats.ProjectileSpeed;
            component.ProjectileDuration += _buffedStats.ProjectileDuration;
            component.ThrustStrength += _buffedStats.ThrustStrength;
            component.BonusHP += _buffedStats.BonusHP;
            component.BonusAcceleration += _buffedStats.BonusAcceleration;
            component.BonusRotationalAcceleration += _buffedStats.BonusRotationalAcceleration;
            component.BonusMaxSpeed += _buffedStats.BonusMaxSpeed;
            component.BonusMaxRotationalSpeed += _buffedStats.BonusMaxRotationalSpeed;
        }
    }
    List<ShipComponent> GetBuffedComponents()
    {
        List<ShipComponent> components = new();

        if (_buffUp)
        {
            ShipComponent c = FindComponent(Vector2Int.up);
            if (c != null)
            {
                components.Add(c);
            }
        }
        if (_buffDown)
        {
            ShipComponent c = FindComponent(Vector2Int.down);
            if (c != null)
            {
                components.Add(c);
            }
        }
        if (_buffRight)
        {
            ShipComponent c = FindComponent(Vector2Int.right);
            if (c != null)
            {
                components.Add(c);
            }
        }
        if (_buffLeft)
        {
            ShipComponent c = FindComponent(Vector2Int.left);
            if (c != null)
            {
                components.Add(c);
            }
        }

        return components;
    }
    ShipComponent FindComponent(Vector2Int position)
    {
        float angle = transform.eulerAngles.z;
        Vector3 rotatedPosition = new Vector3(Mathf.Cos(angle + Mathf.Acos(position.x)), Mathf.Sin(angle + Mathf.Asin(position.y)));
        Vector2 checkLocation = transform.position + rotatedPosition;
        Collider2D hit = Physics2D.OverlapCircle(checkLocation, overlapCircleSize);
        if (hit != null)
        {
            Debug.Log("Collided with: " + hit.GetInstanceID());
            ShipComponent componentToBuff = hit.GetComponent<ShipComponent>();
            if (componentToBuff != null && componentToBuff.CompareTag("Player"))
            {
                return componentToBuff;
            }
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, overlapCircleSize);
        Gizmos.DrawSphere((Vector2)transform.position + Vector2.up, overlapCircleSize);
        Gizmos.DrawSphere((Vector2)transform.position + Vector2.down, overlapCircleSize);
    }
}
