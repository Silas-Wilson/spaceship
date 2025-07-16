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
            Debug.Log($"Buffing {component.name}");
            component.Stats.Mass += _buffedStats.Mass;
            component.Stats.Defense += _buffedStats.Defense;
            component.Stats.Damage += _buffedStats.Damage;
            component.Stats.FireRate += _buffedStats.FireRate;
            component.Stats.ProjectileSpeed += _buffedStats.ProjectileSpeed;
            component.Stats.ProjectileDuration += _buffedStats.ProjectileDuration;
            component.Stats.ThrustStrength += _buffedStats.ThrustStrength;
            component.Stats.BonusHP += _buffedStats.BonusHP;
            component.Stats.BonusAcceleration += _buffedStats.BonusAcceleration;
            component.Stats.BonusRotationalAcceleration += _buffedStats.BonusRotationalAcceleration;
            component.Stats.BonusMaxSpeed += _buffedStats.BonusMaxSpeed;
            component.Stats.BonusMaxRotationalSpeed += _buffedStats.BonusMaxRotationalSpeed;
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
