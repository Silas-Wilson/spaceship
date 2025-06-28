using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    [SerializeField] float _mass;
    [SerializeField] float _defense;
    //Location of core will always be (0, 0)
    [SerializeField] Vector2Int _location;
    public float GetMass()
    {
        return _mass;
    }
}
