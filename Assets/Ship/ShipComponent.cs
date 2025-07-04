using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    [SerializeField] float _mass;
    [SerializeField] float _defense;
    [SerializeField] Draggable _draggable;
    BoxCollider2D _col;
    void Start()
    {
        _col = GetComponent<BoxCollider2D>();
        _col.compositeOperation = Collider2D.CompositeOperation.Merge;
    }
    public float GetMass()
    {
        return _mass;
    }
    public Draggable GetDraggable()
    {
        return _draggable;
    }
}
