using UnityEngine;
using System.Collections.Generic;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] ShipComponent component;
    [SerializeField] LayerMask _ignoredLayers;
    List<Health> enemyHealths = new();
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered");
        if (((1 << collision.gameObject.layer) & _ignoredLayers) != 0)
        {
            return;
        }
        Health collisionHealth = collision.GetComponent<Health>();
        if (collisionHealth == null)
        {
            return;
        }
        enemyHealths.Add(collisionHealth);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if  (((1 << collision.gameObject.layer) & _ignoredLayers) != 0)
        {
            return;
        }
        Health collisionHealth = collision.GetComponent<Health>();
        if (collisionHealth == null)
        {
            return;
        }
        enemyHealths.Remove(collisionHealth);
    }
    void Update()
    {
        Health[] enemyHealthsArray = enemyHealths.ToArray();
        foreach (Health health in enemyHealthsArray)
        {
            if (health == null)
            {
                continue;
            }
            health.TakeDamage(component.Stats.Damage * Time.deltaTime);
        }
    }
}
