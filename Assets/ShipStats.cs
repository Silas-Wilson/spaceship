using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [SerializeField] float _totalMass;
    [SerializeField] float _totalHP;
    [SerializeField] Vector2 _centerOfMass;
    [SerializeField] Rigidbody2D rb;
    public void UpdateShipStats()
    {
        _totalMass = FindTotalMass();
        rb.mass = _totalMass;
    }
    float FindTotalMass()
    {
        float mass = 0;
        ShipComponent[] components = ShipBuildData.Instance.Grid.GetAllComponents();

        foreach (ShipComponent component in components)
        {
            mass += component.GetMass();
        }

        return mass;
    }
}
