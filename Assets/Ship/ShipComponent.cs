using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    [SerializeField] float _mass;
    [SerializeField] float _defense;
    [SerializeField] Draggable _draggable;
    public float GetMass()
    {
        return _mass;
    }
    public Draggable GetDraggable()
    {
        return _draggable;
    }
}
