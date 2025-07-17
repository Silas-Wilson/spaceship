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
    Vector2 overlapBoxSize = new(0.1f, 0.1f);
    public void ApplyBuffs()
    {
        List<ShipComponent> componentsToBuff = GetBuffedComponents();

        foreach (ShipComponent component in componentsToBuff)
        {
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
        Vector2Int thisPosition = ShipBuildData.Instance.Grid.GetLocationOf(_component);
        Debug.Log(thisPosition);
        if (thisPosition == Vector2Int.zero)
        {
            return null;
        }
        ShipComponent c = ShipBuildData.Instance.Grid.GetComponentAt(thisPosition + position);
        if (c != null)
        {
            return c;
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, overlapBoxSize);
        Gizmos.DrawCube((Vector2)transform.position + Vector2.up, overlapBoxSize);
        Gizmos.DrawCube((Vector2)transform.position + Vector2.down, overlapBoxSize);
    }
}
