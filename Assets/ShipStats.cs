using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [SerializeField] float _totalMass;
    [SerializeField] float _totalHP;
    [SerializeField] Vector2 _centerOfMass;
    [SerializeField] Rigidbody2D rb;
    void Start()
    {
        _totalMass = FindTotalMass();
        rb.mass = _totalMass;
    }
    float FindTotalMass()
    {
        float mass = 0;
        ShipComponent[] components = GetComponentsInChildren<ShipComponent>();

        foreach (ShipComponent component in components)
        {
            mass += component.GetMass();
        }

        return mass;
    }
}
