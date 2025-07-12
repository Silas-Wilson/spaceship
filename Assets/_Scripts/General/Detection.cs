using UnityEngine;
using System.Collections.Generic;

public class Detection : MonoBehaviour
{
    [SerializeField] CircleCollider2D coll;
    public List<GameObject> DetectedEntities { get; private set; } = new();

    void OnTriggerEnter2D(Collider2D collision)
    {
        DetectedEntities.Add(collision.gameObject);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        DetectedEntities.Remove(collision.gameObject);
    }
}
