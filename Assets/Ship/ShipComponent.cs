using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    [SerializeField] float _mass;
    [SerializeField] float _defense;
    public float GetMass()
    {
        return _mass;
    }
    public float GetDefense()
    {
        return _defense;
    }
}
